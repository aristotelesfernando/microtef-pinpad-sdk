using System;
using Pinpad.Sdk.Model.Pinpad;
using MicroPos.CrossPlatform;
using System.IO;

namespace Pinpad.Sdk.Pinpad
{
    // TODO: Doc
    public sealed class PinpadUpdateService : IPinpadUpdateService
    {
        private byte [] ApplicationFile { get; set; }
        private int FileCount;
        private IStorageController FileSystemManager { get { return CrossPlatformController.StorageController; } }

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

        public bool Load(string filePath)
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
    }
}
