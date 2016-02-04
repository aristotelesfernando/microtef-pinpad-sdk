using PinPadSDK.Controllers;
using PinPadSDK.Exceptions;
using PinPadSDK.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.PinPad;

namespace PinPadSDK.PinPad {
    /// <summary>
    /// PinPad Printer tool
    /// </summary>
    public class PinPadPrinter {
        /// <summary>
        /// Owner Facade
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        private Nullable<bool> _printerSupported;

        /// <summary>
        /// Is the printer supported?
        /// </summary>
        public bool PrinterSupported {
            get {
                if (_printerSupported.HasValue == false) {
                    _printerSupported = this.IsPrinterSupported();
                }
                if (_printerSupported.HasValue == true) {
                    return _printerSupported.Value;
                }
                else {
                    return false;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public PinPadPrinter(PinPadFacade pinPad) {
            this.PinPad = pinPad;
        }

        private Nullable<bool> IsPrinterSupported( ) {
            if (this.PinPad.Communication.StoneVersion == null){
                return null;
            }
            else if (this.PinPad.Communication.StoneVersion < new PrtRequest().MinimumStoneVersion) {
                return false;
            }

            switch (this.PinPad.Infos.ModelVersion.Trim()) {
                case "D210":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Prepares the Printer to receive data
        /// </summary>
        /// <returns>true on sucess</returns>
        public bool Begin( ) {
            PrtRequest request = new PrtRequest( );
            request.BeginData = new PrtBeginRequestData( );

            return this.SendPrt(request);
        }

        /// <summary>
        /// Prints the data at the buffer
        /// </summary>
        /// <returns>true on sucess</returns>
        public bool End( ) {
            PrtRequest request = new PrtRequest( );
            request.EndData = new PrtEndRequestData( );

            return this.SendPrt(request);
        }

        /// <summary>
        /// Appends a string to the print buffer
        /// </summary>
        /// <param name="message">string to append</param>
        /// <param name="size">Size of the message</param>
        /// <param name="alignment">Alignment of the message</param>
        /// <returns>true on sucess</returns>
        public bool AppendString(string message, PrtStringSize size, PrtAlignment alignment) {
            PrtRequest request = new PrtRequest( );

            PrtAppendStringRequestData appendStringData = new PrtAppendStringRequestData( );
            appendStringData.PRT_MSG.Value = message;
            appendStringData.PRT_SIZE.Value = size;
            appendStringData.PRT_ALIGNMENT.Value = alignment;

            request.AppendStringData = appendStringData;

            return this.SendPrt(request);
        }

        /// <summary>
        /// Appends a image to the print buffer
        /// </summary>
        /// <param name="fileName">Image file at the PinPad</param>
        /// <param name="horizontal">Left padding in pixels</param>
        /// <returns>true on sucess</returns>
        public bool AppendImage(string fileName, int horizontal) {
            PrtRequest request = new PrtRequest( );

            PrtAppendImageRequestData appendImageData = new PrtAppendImageRequestData( );
            appendImageData.PRT_FILENAME.Value = fileName;
            appendImageData.PRT_PADDING.Value = horizontal;

            request.AppendImageData = appendImageData;

            return this.SendPrt(request);
        }

        /// <summary>
        /// Sets the vertical offset for the next append
        /// </summary>
        /// <param name="steps">Vertical offset for the next append, can be negative</param>
        /// <returns>true on sucess</returns>
        public bool Step(int steps) {
            PrtRequest request = new PrtRequest( );

            PrtStepRequestData stepData = new PrtStepRequestData( );
            stepData.PRT_STEPS.Value = steps;

            request.StepData = stepData;

            return this.SendPrt(request);
        }

        private bool SendPrt(PrtRequest request) {
            PrtResponse response = this.PinPad.Communication.SendRequestAndReceiveResponse<PrtResponse>(request);

            if (response == null) {
                return false;
            }
            else if (response.RSP_STAT.Value != ResponseStatus.ST_OK) {
                return false;
            }
            else if (response.BeginData != null) {
                if (response.BeginData.PRT_STATUS.Value == PinPadPrinterStatus.Ready) {
                    return true;
                }
                else {
                    throw PrinterExceptionFactory.Create(response.BeginData.PRT_STATUS.Value);
                }
            }
            else {
                return true;
            }
        }
    }
}