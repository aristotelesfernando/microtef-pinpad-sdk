using System;
using Pinpad.Sdk.Model.Pinpad;
using MicroPos.CrossPlatform;
using System.IO;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands.Request;
using Pinpad.Sdk.Commands.Response;
using MicroPos.CrossPlatform.TypeCode;
using System.Diagnostics;

namespace Pinpad.Sdk.Pinpad
{
    /// <summary>
    /// Responsible for providing methods and informations to update the pinpad embedded application.
    /// </summary>
    public sealed class PinpadUpdateService : IPinpadUpdateService
    {
        // public properties:
        /// <summary>
        /// Size of the package to be sent to the pinpad.
        /// Each package is a piece of the zipped application.
        /// </summary>
        public int SectionSize { get { return 900; } }
        /// <summary>
        /// Current application version running in the pinpad.
        /// </summary>
        public Version CurrentApplicationVersion
        {
            get
            {
                if (this.currentApplicationVersion == null)
                {
                    this.currentApplicationVersion = this.GetVersion();
                }

                return this.currentApplicationVersion;
            }
        }

        // internal and private properties:
        /// <summary>
        /// Stores the number of bytes already read in the <see cref="ApplicationFile"/> property, by the
        /// <see cref="GetNextPackageSection"/>.
        /// </summary>
        private int FileCount { get; set; }
        /// <summary>
        /// All bytes of the zipped new application file.
        /// </summary>
        private byte [] ApplicationFile { get; set; }
        /// <summary>
        /// Responsible for managing files in the current platform.
        /// </summary>
        private IStorageController FileSystemManager
        {
            get { return CrossPlatformController.StorageController; }
        }
        /// <summary>
        /// Informations about the device.
        /// </summary>
        private IPinpadInfos PinpadInfos { get; set; }
        /// <summary>
        /// Responsible for sending and receiving commands to and from the pinpad.
        /// </summary>
        private IPinpadCommunication PinpadCommunication { get; set; }

        /// <summary>
        /// Stores the application version running in the pinpad.
        /// </summary>
        private GavResponse rawPinpadAppVersion;
        /// <summary>
        /// Returns the value <see cref="rawPinpadAppVersion"/>. Sends a new <see cref="GavRequest"/> if the
        /// property is null.
        /// </summary>
        private GavResponse RawPinpadAppVersion
        {
            get
            {
                if (this.rawPinpadAppVersion == null)
                {
                    this.rawPinpadAppVersion = this.GetGav();
                }

                return this.rawPinpadAppVersion;
            }
        }

        /// <summary>
        /// Current application version running in the pinpad.
        /// </summary>
        private Version currentApplicationVersion;

        // ctor:
        /// <summary>
        /// Creates the instance mandatorily with a <see cref="IPinpadInfos"/> and <see cref="IPinpadCommunication"/>.
        /// Otherwise, throws exception.
        /// </summary>
        /// <param name="pinpadInformation">Informations about the device.</param>
        /// <param name="pinpadCommunication">Responsible for sending and receiving commands to and from the 
        /// pinpad.</param>
        /// <exception cref="ArgumentException">If any parameters are null.</exception>
        public PinpadUpdateService (IPinpadInfos pinpadInformation, IPinpadCommunication pinpadCommunication)
        {
            if (pinpadInformation == null)
            {
                throw new ArgumentNullException("pinpadInformation");
            }
            if (pinpadCommunication == null)
            {
                throw new ArgumentNullException("pinpadCommunication");
            }

            this.PinpadInfos = pinpadInformation;
            this.PinpadCommunication = pinpadCommunication;
        }

        // methods:
        /// <summary>
        /// Loads the zipped file in the memory.
        /// </summary>
        /// <param name="filePath">Absolute file path of the new application.</param>
        public bool Load (string filePath)
        {
            // Verify if the file exist:
            if (this.FileSystemManager.FileExist(filePath) == false) { return false; }

            try
            {
                // Read bytes of file:
                using (Stream appFileBytes = this.FileSystemManager.OpenFile(filePath))
                {
                    // Saves file bytes in the property:
                    using (MemoryStream ms = new MemoryStream())
                    {
                        appFileBytes.CopyTo(ms);
                        this.ApplicationFile = ms.ToArray();
                    }
                }

                // Reset the file count:
                this.FileCount = 0;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Updates the pinpad with the application previously loaded. 
        /// Must be called after the <see cref="Load(string)"/>.
        /// </summary>
        /// <returns>Whether the update was successful.</returns>
        /// <exception cref="InvalidOperationException">If the <see cref="Load(string)"/> was not called yet.
        /// </exception>
        public bool Update ()
        {
            // Verify if it is a WiFi Pinpad (eligible for this operation):
            if (this.PinpadInfos.IsStoneProprietaryDevice == false) { return false; }

            // If the file is null, Load() was not called:
            if (this.ApplicationFile == null)
            {
                throw new InvalidOperationException("Load was not performed.");
            }

            // Send the Update Init (UPI):
            UpiRequest upiRequest = new UpiRequest();
            upiRequest.UPI_APPSIZE.Value = this.ApplicationFile.Length;
                
            if (this.PinpadCommunication.SendRequestAndVerifyResponseCode(upiRequest) == true)
            {
                byte[] nextPackage;

                // Send the Update Recods (UPRs), loading all parts of the file:
                do
                {
                    // Get next package:
                    nextPackage = this.GetNextPackageSection();

                    if (nextPackage == null) { continue; }

                    // Create the UPR request...
                    UprRequest uprRequest = new UprRequest();
                    uprRequest.UPR_REC.Value = nextPackage;

                    // ... And send the next section:
                    this.PinpadCommunication.SendRequestAndVerifyResponseCode(uprRequest);

                    Debug.WriteLine("Count {0} out of {1} | Pack length {2}", this.FileCount, 
                        this.ApplicationFile.Length, nextPackage.Length);
                }
                while (nextPackage != null);

                // Send the Update End (UPE):
                return this.PinpadCommunication.SendRequestAndVerifyResponseCode(new UpeRequest());
            }

            return false;
        }
        /// <summary>
        /// Gets the next section of the package.
        /// </summary>
        /// <returns>The next package</returns>
        private byte[] GetNextPackageSection()
        {
            int bytesToCopy = this.SectionSize;

            // If the file were already read, then there is no section left to read:
            if (this.FileCount == this.ApplicationFile.Length) { return null; }

            // If there isn't another full section, that is, the current section is smaller than the
            // default size of a section, then the application must read only the remaining bytes:
            if (ApplicationFile.Length - this.FileCount < this.SectionSize)
            {
                // Then read a full section:
                bytesToCopy = ApplicationFile.Length - this.FileCount;
            }

            byte[] partial = new byte[bytesToCopy];
            Array.Copy(this.ApplicationFile, FileCount, partial, 0, bytesToCopy);
            this.FileCount += bytesToCopy;

            return partial;
        }
        // private folks:
        /// <summary>
        /// Sends a <see cref="GavRequest"/> and gets it response.
        /// </summary>
        /// <returns></returns>
        private GavResponse GetGav()
        {
            // Creates GAV request,
            // Sends the GAV request to the pinpad
            // And returns it's response:
            return this.PinpadCommunication.SendRequestAndReceiveResponse<GavResponse>(new GavRequest());
        }
        /// <summary>
        /// Tryies to parse the pinpad version (as string) returned in a <see cref="GavResponse"/> into a 
        /// <see cref="Version"/>. If the parse was not successful, then retuns the default version "0.0.0".
        /// </summary>
        /// <returns>Application version running in the pinpad.</returns>
        private Version GetVersion()
        {
            Version appVer;

            if (this.RawPinpadAppVersion == null ||
                Version.TryParse(this.RawPinpadAppVersion.GAV_APPVER?.Value, out appVer) == false)
            {
                appVer = Version.Parse("0.0.0");
            }

            return appVer;
        }
    }
}
