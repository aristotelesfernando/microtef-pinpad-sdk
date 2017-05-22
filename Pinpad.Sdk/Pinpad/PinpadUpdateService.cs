using System;
using Pinpad.Sdk.Model.Pinpad;
using MicroPos.CrossPlatform;
using System.IO;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands.Request;
using Pinpad.Sdk.Commands.Response;
using System.Text;
using MicroPos.CrossPlatform.TypeCode;

namespace Pinpad.Sdk.Pinpad
{
    // TODO: Doc
    public sealed class PinpadUpdateService : IPinpadUpdateService
    {
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
                if (this.SectionSize == this.ApplicationFile.Length) { return null; }
                
                // If there isn't another full section, that is, the current section is smaller than the
                // default size of a section, then the application must read only the remaining bytes:
                if (this.FileCount - ApplicationFile.Length < this.SectionSize)
                {
                    // Then read a full section:
                    bytesToCopy = this.FileCount - ApplicationFile.Length;
                }

                byte[] partial = new byte[bytesToCopy];
                Array.Copy(this.ApplicationFile, partial, bytesToCopy);
                this.FileCount += bytesToCopy;

                return partial;
            }
        }

        public PinpadUpdateService (IPinpadInfos pinpadInformation)
        {
            this.PinpadInfos = pinpadInformation;
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

            // Send the GAV:
            GavRequest gavRequest = new GavRequest();
            GavResponse gavResponse = this.PinpadCommunication
                .SendRequestAndReceiveResponse<GavResponse>(gavRequest);
            
            if (this.ApplicationNameAndVersion.Contains(gavResponse?.GAV_APPVER.Value) == true)
            {
                // Send the Update Init (UPI):
                UpiRequest upiRequest = new UpiRequest();
                
                if (this.PinpadCommunication.SendRequestAndVerifyResponseCode(upiRequest) == true)
                {
                    // Send the Update Recods (UPRs), loading all parts of the file:
                    while (this.NextPackageSection != null)
                    {
                        UprRequest uprRequest = new UprRequest();
                        uprRequest.UPR_REC.Value.Add(CrossPlatformController.TextEncodingController
                            .GetString(TextEncodingType.Ascii, this.NextPackageSection));
                        this.PinpadCommunication.SendRequestAndVerifyResponseCode(uprRequest);
                    }

                    // Send the Update End (UPE):
                    return this.PinpadCommunication.SendRequestAndVerifyResponseCode(new UpeRequest());
                }
            }

            return false;
        }
    }
}
