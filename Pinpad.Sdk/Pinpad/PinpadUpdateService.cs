using System;
using Pinpad.Sdk.Model.Pinpad;

namespace Pinpad.Sdk.Pinpad
{
    // TODO: Doc
    public sealed class PinpadUpdateService : IPinpadUpdateService
    {
        private byte [] File { get; set; }
        private int FileCount;

        public int SectionSize { get { return 900; } }
        public byte[] NextPackageSection
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Load(string filePath)
        {
            // TODO: Verify if the file exist

            // TODO: Read bytes of file

            // TODO: Saves file bytes in the property

            //
            throw new NotImplementedException();
        }
    }
}
