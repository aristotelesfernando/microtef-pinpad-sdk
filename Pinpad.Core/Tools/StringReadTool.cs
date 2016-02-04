using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class StringReadTool
    {
        string Data;
        int CurrentOffset;

        public StringReadTool(string Data)
        {
            this.Data = Data;
            this.CurrentOffset = 0;
        }

        public bool IsOver()
        {
            if (Remaining() <= 0)
                return true;
            else
                return false;
        }

        public int Remaining()
        {
            return Data.Length - CurrentOffset;
        }

        public void Jump(int Length)
        {
            CurrentOffset += Length;
        }

        public string ReadString(int Length)
        {
            string Substring = Data.Substring(CurrentOffset, Length);
            Jump(Length);
            return Substring;
        }

        public long ReadLong(int Length)
        {
            string Substring = ReadString(Length);
            long Value = Convert.ToInt64(Substring);
            return Value;
        }

        public int ReadInt(int Length)
        {
            string Substring = ReadString(Length);
            int Value = Convert.ToInt32(Substring);
            return Value;
        }

        public bool ReadBool()
        {
            int Value = ReadInt(1);

            if (Value == 0)
                return false;
            else
                return true;
        }
    }
}
