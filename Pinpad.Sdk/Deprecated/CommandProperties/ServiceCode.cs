using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PinPadSDK.Property {
    /// <summary>
    /// Controller for Service Codes
    /// </summary>
    internal class ServiceCode {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">Service Code to read</param>
        internal ServiceCode(string code) {
            this.Code = code;
        }

        private string _code;

        /// <summary>
        /// The service code string in the controller
        /// </summary>
        internal string Code {
            get {
                return this._code;
            }
            set {
                if (value == null || value.Length != 3) {
                    throw new InvalidOperationException("ServiceCode does not support codes that are not 3 digits long");
                }
                this._code = value;
            }
        }

        /// <summary>
        /// Interchange
        /// </summary>
        internal InterchangeType Interchange {
            get {
                switch (this.GetPositionValue(0)) {
                    case 1:
                    case 2:
                        return InterchangeType.International;

                    case 5:
                    case 6:
                        return InterchangeType.National;

                    case 7:
                        return InterchangeType.Private;

                    case 9:
                        return InterchangeType.Test;

                    default:
                        return InterchangeType.Undefined;
                }
            }
        }

        /// <summary>
        /// Does the service code contains a EMV chip?
        /// </summary>
        internal bool IsEMV {
            get {
                switch (this.GetPositionValue(0)) {
                    case 2:
                    case 6:
                        return true;

                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Is the Pin required for this card?
        /// </summary>
        internal bool IsPinRequired {
            get {
                switch (this.GetPositionValue(2)) {
                    case 0:
                    case 3:
                    case 5:
                    case 6:
                    case 7:
                        return true;

                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Is the pin mandatory?
        /// Some cases are not required to get the Pin if a Pin Entry Device is not present
        /// </summary>
        internal bool IsPinMandatory {
            get {
                switch (this.GetPositionValue(2)) {
                    case 0:
                    case 3:
                    case 5:
                        return true;

                    default:
                        return false;
                }
            }
        }

        private int GetPositionValue(int position) {
            string digit = Code.Substring(position, 1);
            return Convert.ToInt32(digit); ;
        }

        /// <summary>
        /// String formatter for ServiceCodeController
        /// </summary>
        /// <param name="obj">object to convert</param>
        /// <returns>Value of the property as string</returns>
        internal static string StringFormatter(ServiceCode obj) {
            return obj.Code;
        }

        /// <summary>
        /// String parser for ServiceCodeController
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>ServiceCodeController</returns>
        internal static ServiceCode StringParser(StringReader reader) {
            string firstChar = reader.PeekString(1);
            if (Regex.IsMatch(firstChar, @"\d") == false) { //If the first char is not a numeric char then consider it a separator and the value null
                return null;
            }
            else {
                string strValue = reader.ReadString(3);
                ServiceCode value = new ServiceCode(strValue);
                return value;
            }
        }
    }
}
