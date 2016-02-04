using CrossPlatformBase;
using Dlp.Buy4.Switch.Utils;
using System;
using System.Collections.Generic;

namespace PinPadSDK.Tools
{
    public class EmvReadTool
    {
        public EmvReadTool(string EMV)
            : this(CrossPlatformController.TextEncodingController.GetBytes(TextEncoding.ASCII,EMV))
        {
        }
        public EmvReadTool(byte[] EMV)
        {
            EMVData = new Dictionary<string, string>();

            if (EMV == null || EMV.Length == 0)
                return;

            ReadTool readTool = new ReadTool(EMV);
            byte[] Byte = new byte[1];
            Byte[0] = readTool.ReadByte();
            while (readTool.Left() > 0)
            {
                string TAG = "";
                bool done = false;
                while(!done)
                {
                    if ((Byte[0] & 0xF) != 15) //0000 1111
                        done = true;
                    TAG += HexUtils.FromBytes(Byte);
                    Byte[0] = readTool.ReadByte();
                }
                string LENGHT = "";
                done = false;
                while (!done)
                {
                    if ((Byte[0] & 0x80) == 0) //1000 0000
                        done = true;
                    else
                        Byte[0] &= 0x7F; //0111 1111
                    LENGHT += HexUtils.FromBytes(Byte);
                    Byte[0] = readTool.ReadByte();
                }
                int lenght = Convert.ToInt32(LENGHT, 16);
                string VALUE = "";
                for (int i = 0; i < lenght; i++)
                {
                    VALUE += HexUtils.FromBytes(Byte);
                    if(readTool.Left() > 0)
                        Byte[0] = readTool.ReadByte();
                }
                if (EMVData.ContainsKey(TAG))
                    EMVData[TAG] = VALUE;
                else
                    EMVData.Add(TAG, VALUE);
            }
        }
        public List<string> GetTags()
        {
            List<string> TagList = new List<string>();
            foreach (string tag in EMVData.Keys)
            {
                TagList.Add(tag);
            }
            return TagList;
        }
        public string GetTagString()
        {
            string tagstring = "";
            foreach (string tag in EMVData.Keys)
            {
                tagstring += tag;
            }
            return tagstring;
        }
        public string this[string Tag]
        {
            get
            {
                try
                {
                    return EMVData[Tag];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
        }
        protected Dictionary<string, string> EMVData;
    }
}
