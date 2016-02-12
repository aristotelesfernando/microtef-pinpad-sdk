using System;

/*
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */ 

namespace Pinpad.Core.Properties
{
    /// <summary>
    /// Controller for reading a string containing parameters
    /// </summary>
    public class StringReader 
	{
		// Members
        /// <summary>
        /// Original string
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// The last read performed
        /// </summary>
        public string LastReadString { get; private set; }
        /// <summary>
        /// Current parameter offset
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Remaining characters in the string
        /// </summary>
        public int Remaining { get { return this.Value.Length - this.Offset; } }
        /// <summary>
        /// True if the string was fully read
        /// </summary>
        public bool IsOver { get { return Remaining == 0; } }
        
		// Methods
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">string to read</param>
        public StringReader(string value) {
            this.Value = value;
            this.LastReadString = String.Empty;
            this.Offset = 0;
        }
        /// <summary>
        /// Reads a substring
        /// </summary>
        /// <param name="length">length to read</param>
        /// <returns>string</returns>
        public string ReadString(int length) {
            this.LastReadString = String.Empty;
            string value = this.Value.Substring(this.Offset, length);
            this.Jump(length);
            this.LastReadString = value;
            return value;
        }
        /// <summary>
        /// Reads a substring without changing the offset
        /// </summary>
        /// <param name="length">length to read</param>
        /// <returns>string</returns>
        public string PeekString(int length) {
            this.LastReadString = String.Empty;
            string value = this.Value.Substring(this.Offset, length);
            this.LastReadString = value;
            return value;
        }
        /// <summary>
        /// Reads a long integer
        /// </summary>
        /// <param name="length">length to read</param>
        /// <returns>long</returns>
        public long ReadLong(int length) {
            string substring = this.ReadString(length);
            long value = Convert.ToInt64(substring);
            return value;
        }
        /// <summary>
        /// Reads a long integer without changing the offset
        /// </summary>
        /// <param name="length">length to read</param>
        /// <returns>long</returns>
        public long PeekLong(int length) {
            string substring = this.PeekString(length);
            long value = Convert.ToInt64(substring);
            return value;
        }
        /// <summary>
        /// Reads a integer
        /// </summary>
        /// <param name="length">length to read</param>
        /// <returns>integer</returns>
        public int ReadInt(int length) {
            string substring = this.ReadString(length);
            int value = Convert.ToInt32(substring);
            return value;
        }
        /// <summary>
        /// Reads a integer without changing the offset
        /// </summary>
        /// <param name="length">length to read</param>
        /// <returns>integer</returns>
        public int PeekInt(int length) {
            string substring = this.PeekString(length);
            int value = Convert.ToInt32(substring);
            return value;
        }
        /// <summary>
        /// Reads a boolean
        /// </summary>
        /// <returns>boolean</returns>
        public bool ReadBool() {
            int value = this.ReadInt(1);

            return value != 0;
        }
        /// <summary>
        /// Reads a boolean without changing the offset
        /// </summary>
        /// <returns>boolean</returns>
        public bool PeekBool() {
            int value = this.PeekInt(1);

            return value != 0;
        }
        /// <summary>
        /// Skips the specified length or return if length is negative
        /// </summary>
        /// <param name="length">length to skip or return if negative</param>
        public void Jump(int length) {
            this.Offset += length;
        }
    }
}
