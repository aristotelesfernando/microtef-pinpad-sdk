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
                throw new NotImplementedException();
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

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
