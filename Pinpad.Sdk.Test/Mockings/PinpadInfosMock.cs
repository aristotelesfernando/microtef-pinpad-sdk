using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk.Test.Mockings
{
    public sealed class PinpadInfosMock : IPinpadInfos
    {
        public bool IsContactless
        {
            get
            {
                return false;
            }
        }
        public bool IsStoneProprietaryDevice { get; private set; }
        public bool IsStoneSupported
        {
            get
            {
                return true;
            }
        }
        public string ManufacturerName
        {
            get
            {
                return "INGENICO";
            }
        }
        public string ManufacturerVersion
        {
            get
            {
                return "12234.122";
            }
        }
        public string Model
        {
            get
            {
                return "iWL250";
            }
        }
        public string OperatingSystemVersion
        {
            get
            {
                return "1.08a";
            }
        }
        public string SerialNumber
        {
            get
            {
                return "14169WL28445784";
            }
        }
        public string Specifications
        {
            get
            {
                return "123.568.44";
            }
        }
        public string GetDukptSerialNumber(int indexToSearch, CryptographyMode cryptographyMode)
        {
            return "mocked KSN";
        }

        public PinpadInfosMock(bool isFromStone = true)
        {
            this.IsStoneProprietaryDevice = isFromStone;
        }
    }
}
