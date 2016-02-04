using CrossPlatformBase;
using PinPadSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class DataTools
    {
        public static string DisplayByteArray(byte[] barray, string start = "", string end = "")
        {
            string msg = start + barray.Length + " - ";
            foreach (byte b in barray)
                msg += b.ToString("X") + " ";
            msg += end;
            return msg;
        }

        public static string CenterString(string source, int length, char paddingChar = ' ')
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft, paddingChar).PadRight(length, paddingChar);
        }

        public static byte[] ToASCII(string value)
        {
            return CrossPlatformController.TextEncodingController.GetBytes(TextEncoding.ASCII, value);
        }

        public static string FromASCII(byte[] value)
        {
            return CrossPlatformController.TextEncodingController.GetString(TextEncoding.ASCII, value);
        }

        public static byte[] ToASCII(string value, int size, char? Pad = ' ', bool PadRight = true)
        {
            string strvalue;
            if (Pad.HasValue)
            {
                if (PadRight)
                    strvalue = value.PadRight(size, Pad.Value);
                else
                    strvalue = value.PadLeft(size, Pad.Value);
            }
            else
                strvalue = value;
            if (strvalue.Length > size)
                throw new PinPadExcedingSizeException("Size bigger than expected. string: '" + strvalue + "' size: " + strvalue.Length + " expected: " + size);
            return CrossPlatformController.TextEncodingController.GetBytes(TextEncoding.ASCII, strvalue);
        }

        public static byte[] ToASCIIChecksum(string msg)
        {
            List<byte> bytelst = new List<byte>();
            byte[] msgbytes = CrossPlatformController.TextEncodingController.GetBytes(TextEncoding.ASCII, msg);
            List<byte> msgbytelst = msgbytes.ToList();

            msgbytelst.Add(0x17);
            msgbytes = msgbytelst.ToArray();
            UInt16 checksum = Checksum(msgbytes);

            bytelst.AddRange(msgbytes);
            bytelst.AddRange(BitConverter.GetBytes(checksum).Reverse());

            bytelst.Insert(0, 0x16);

            return bytelst.ToArray();
        }

        public static byte[] LongToByte(long value, int size)
        {
            string strvalue = value.ToString().PadLeft(size, '0');
            if (strvalue.Length > size)
                throw new PinPadExcedingSizeException("Size bigger than expected. string: '" + strvalue + "' size: " + strvalue.Length + " expected: " + size);
            return ToASCII(strvalue);
        }

        public static byte[] BoolToByte(bool value)
        {
            if (value)
                return ToASCII("1");
            else
                return ToASCII("0");
        }

        public static List<string> BreakString(string value, int length)
        {
            int AmountOfLines = value.Length / length;
            List<string> Lines = new List<string>();
            for (int i = 0; i <= AmountOfLines; i++)
            {
                if (value.Length >= (i + 1) * length)
                    Lines.Add(value.Substring(i * length, length));
                else
                    Lines.Add(value.Substring(i * length));
            }
            return Lines;
        }

        public static UInt16 Checksum(byte[] data, int size, int offset = 0)
        {
            const UInt16 mask = 0x1021; // x^16 + x^12 + x^5 + x^0
            UInt16 crc = 0, value;

            // Iterate each byte.
            for (int i = offset; i < size + offset; ++i)
            {
                value = (UInt16)(data[i] << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((crc ^ value) & 0x8000) != 0)
                        crc = (UInt16)((crc << 1) ^ mask);
                    else
                        crc <<= 1;

                    value <<= 1;
                }
            }

            return crc;
        }

        public static UInt16 Checksum(byte[] data)
        {
            return Checksum(data, data.Length);
        }
    }
}
