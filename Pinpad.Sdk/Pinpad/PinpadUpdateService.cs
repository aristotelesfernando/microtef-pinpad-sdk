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
    // TODO: Doc
    public sealed class PinpadUpdateService : IPinpadUpdateService
    {
        public const string PinpadPackageNamePattern = @"([a-zA-Z]){1,15}[\(](\d+\.\d+\.\d+)[\)]\.\z\i\p";

        private int FileCount;

        private byte [] ApplicationFile { get; set; }
        private IStorageController FileSystemManager { get { return CrossPlatformController.StorageController; } }
        private IPinpadInfos PinpadInfos { get; set; }
        private IPinpadCommunication PinpadCommunication { get; set; }

        public string ApplicationNameAndVersion { get; set; }

        public int SectionSize { get { return 900; } }
        public byte[] NextPackageSection
        {
            get
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
                Array.Copy(this.ApplicationFile, partial, bytesToCopy);
                this.FileCount += bytesToCopy;

                return partial;
            }
        }

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
                this.ApplicationNameAndVersion = filePath;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update ()
        {
            // Verify if it is a WiFi Pinpad (eligible for this operation):
            if (this.PinpadInfos.IsStoneProprietaryDevice == false) { return false; }

            // If the file is null, Load() was not called:
            if (this.ApplicationFile == null)
            {
                throw new InvalidOperationException("Load was not performed.");
            }

            // Send the GAV:
            GavRequest gavRequest = new GavRequest();
            GavResponse gavResponse = this.PinpadCommunication
                .SendRequestAndReceiveResponse<GavResponse>(gavRequest);

            if (this.ApplicationNameAndVersion.Contains(gavResponse?.GAV_APPVER.Value) == true)
            {
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
                        nextPackage = this.NextPackageSection;

                        if (nextPackage == null) { continue; }

                        // Create the UPR request...
                        UprRequest uprRequest = new UprRequest();
                        uprRequest.UPR_REC.Value = CrossPlatformController.TextEncodingController
                            .GetString(TextEncodingType.Ascii, nextPackage);

                        // ... And send the next section:
                        this.PinpadCommunication.SendRequestAndVerifyResponseCode(uprRequest);

                        Debug.WriteLine("Count {0} out of {1} | Pack length {2}", this.FileCount, 
                            this.ApplicationFile.Length, nextPackage.Length);
                    }
                    while (nextPackage != null);

                    // Send the Update End (UPE):
                    return this.PinpadCommunication.SendRequestAndVerifyResponseCode(new UpeRequest());
                }
            }

            return false;
        }
        
    }
}
