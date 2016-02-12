using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Commands;

namespace PinPadSDK.PinPad {
    /// <summary>
    /// PinPad infos tool
    /// </summary>
    public class PinPadInfos {
        /// <summary>
        /// Owner Facade
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        private Gin00Response _gin00Response { get; set; }

        private Gin00Response gin00Response {
            get {
                if (_gin00Response == null) {
                    _gin00Response = this.GetGin00();
                }
                return _gin00Response;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public PinPadInfos(PinPadFacade pinPad) {
            this.PinPad = pinPad;
        }

        private Gin00Response GetGin00( ) {
            GinRequest request = new GinRequest( );
            request.GIN_ACQIDX.Value = 00;

            Gin00Response response = this.PinPad.Communication.SendRequestAndReceiveResponse<Gin00Response>(request);
            return response;
        }

        /// <summary>
        /// Manufacturer Name
        /// </summary>
        public string ManufacturerName {
            get {
                if (gin00Response == null) {
                    return null;
                }
                else {
                    return gin00Response.GIN_MNAME.Value;
                }
            }
        }

        /// <summary>
        /// Model and Hardware version
        /// </summary>
        public string ModelVersion {
            get {
                if (gin00Response == null) {
                    return null;
                }
                else {
                    return gin00Response.GIN_MODEL.Value;
                }
            }
        }

        /// <summary>
        /// Is Contactless supported?
        /// </summary>
        public bool ContactlessSupported {
            get {
                if (gin00Response == null) {
                    return false;
                }
                else {
                    return gin00Response.GIN_CTLSUP.Value == "C";
                }
            }
        }

        /// <summary>
        /// Basic software or Operational System versions, without a defined format
        /// </summary>
        public string OperationalSystemVersion {
            get {
                if (gin00Response == null) {
                    return null;
                }
                else {
                    return gin00Response.GIN_SOVER.Value;
                }
            }
        }

        /// <summary>
        /// Specification version at format "V.VV" or "VVVA" where A is the alphanumeric identifier, example: "108a"
        /// </summary>
        public string SpecificationVersion {
            get {
                if (gin00Response == null) {
                    return null;
                }
                else {
                    return gin00Response.GIN_SPECVER.Value;
                }
            }
        }

        /// <summary>
        /// Manufactured Version at format "VVV.VV YYMMDD"
        /// </summary>
        public string ManufacturedVersion {
            get {
                if (gin00Response == null) {
                    return null;
                }
                else {
                    return gin00Response.GIN_MANVER.Value;
                }
            }
        }

        /// <summary>
        /// Serial number
        /// </summary>
        public string SerialNumber {
            get {
                if (gin00Response == null) {
                    return null;
                }
                else {
                    return gin00Response.GIN_SERNUM.Value;
                }
            }
        }
    }
}
