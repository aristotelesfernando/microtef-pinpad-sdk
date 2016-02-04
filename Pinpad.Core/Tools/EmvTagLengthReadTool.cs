using CrossPlatformBase;
using Dlp.Buy4.Switch.Utils;
using System;
using System.Collections.Generic;

namespace PinPadSDK.Tools {
    public class EmvTagLengthReadTool {
        private Dictionary<string, int> TagLengthDictionary;

        public EmvTagLengthReadTool(string TagLength)
            : this(CrossPlatformController.TextEncodingController.GetBytes(TextEncoding.ASCII, TagLength)) {
        }
        public EmvTagLengthReadTool(byte[] TagLength) {
            TagLengthDictionary = new Dictionary<string, int>();

            if (TagLength == null || TagLength.Length == 0)
                return;

            ReadTool readTool = new ReadTool(TagLength);
            byte[] Byte = new byte[1];
            while (readTool.Left() > 0) {
                Byte[0] = readTool.ReadByte();
                string tag = "";
                bool done = false;
                while (!done) {
                    if ((Byte[0] & 0xF) != 15) //0000 1111
                        done = true;
                    tag += HexUtils.FromBytes(Byte);
                    Byte[0] = readTool.ReadByte();
                }
                string lengthString = "";
                done = false;
                while (!done) {
                    if ((Byte[0] & 0x80) == 0) //1000 0000
                        done = true;
                    else
                        Byte[0] &= 0x7F; //0111 1111
                    lengthString += HexUtils.FromBytes(Byte);
                }
                int lenght = Convert.ToInt32(lengthString, 16);
                TagLengthDictionary.Add(tag, lenght);
            }
        }

        public List<string> GetTags() {
            List<string> TagList = new List<string>();
            foreach (string tag in TagLengthDictionary.Keys) {
                TagList.Add(tag);
            }
            return TagList;
        }

        public string GetTagString() {
            string tagstring = "";
            foreach (string tag in TagLengthDictionary.Keys) {
                tagstring += tag;
            }
            return tagstring;
        }

        public Nullable<int> this[string Tag] {
            get {
                try {
                    return TagLengthDictionary[Tag];
                }
                catch (KeyNotFoundException) {
                    return null;
                }
            }
        }
    }
}
