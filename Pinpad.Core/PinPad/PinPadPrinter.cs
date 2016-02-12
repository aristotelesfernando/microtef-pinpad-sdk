using Pinpad.Core.Commands;
using Pinpad.Core.Pinpad;
using Pinpad.Core.TypeCode;
using Pinpad.Core.Utilities;
using Pinpad.Sdk.Model.TypeCode;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Pinpad 
{
	/// <summary>
	/// PinPad Printer tool
	/// </summary>
	public class PinpadPrinter 
	{
		// Members
		/// <summary>
		/// Pinpad communication.
		/// </summary>
		private PinpadCommunication communication;
		/// <summary>
		/// Pinpad model.
		/// </summary>
		private string pinpadModel;
		/// <summary>
		/// If pinpad has printer.
		/// </summary>
		private Nullable<bool> ispPinterSupported;
		/// <summary>
		/// If pinpad has printer.
		/// </summary>
		public bool PrinterSupported {
			get 
			{
				if (ispPinterSupported.HasValue == false) 
				{
					ispPinterSupported = this.IsPrinterSupported();
				}
				if (ispPinterSupported.HasValue == true) 
				{
					return ispPinterSupported.Value;
				}
				else 
				{
					return false;
				}
			}
		}

		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinPad">PinPad to use</param>
		public PinpadPrinter(PinpadCommunication communication, string pinpadModel) 
		{
			this.communication = communication;
			this.pinpadModel = pinpadModel;
		}

		// Methods
		private Nullable<bool> IsPrinterSupported() {
			if (this.communication.StoneVersion == null){
				return null;
			}
			else if (this.communication.StoneVersion < new PrtRequest().MinimumStoneVersion) {
				return false;
			}

			switch (this.pinpadModel.Trim()) {
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
		public bool Begin() 
		{
			PrtRequest request = new PrtRequest( );
			request.BeginData = new PrtBeginRequestData();

			return this.SendPrt(request);
		}
		/// <summary>
		/// Prints the data at the buffer
		/// </summary>
		/// <returns>true on sucess</returns>
		public bool End() {
			PrtRequest request = new PrtRequest();
			request.EndData = new PrtEndRequestData();

			return this.SendPrt(request);
		}
		/// <summary>
		/// Appends a string to the print buffer
		/// </summary>
		/// <param name="message">string to append</param>
		/// <param name="size">Size of the message</param>
		/// <param name="alignment">Alignment of the message</param>
		/// <returns>true on sucess</returns>
		public bool AppendString(string message, PrinterStringSize size, PrinterAlignmentCode alignment)
		{
			PrtRequest request = new PrtRequest();

			PrtAppendStringRequestData appendStringData = new PrtAppendStringRequestData();
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
		public bool AppendImage(string fileName, int horizontal) 
		{
			PrtRequest request = new PrtRequest();

			PrtAppendImageRequestData appendImageData = new PrtAppendImageRequestData();
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
		public bool Step(int steps) 
		{
			PrtRequest request = new PrtRequest();

			PrtStepRequestData stepData = new PrtStepRequestData();
			stepData.PRT_STEPS.Value = steps;

			request.StepData = stepData;

			return this.SendPrt(request);
		}
		private bool SendPrt(PrtRequest request) 
		{
			PrtResponse response = this.communication.SendRequestAndReceiveResponse<PrtResponse>(request);

			if (response == null) 
			{
				return false;
			}
			else if (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) 
			{
				return false;
			}
			else if (response.BeginData != null) 
			{
				if (response.BeginData.PRT_STATUS.Value == PinpadPrinterStatus.Ready) 
				{
					return true;
				}
				else 
				{
					throw new PrinterExceptionFactory().Create(response.BeginData.PRT_STATUS.Value);
				}
			}
			else 
			{
				return true;
			}
		}
	}
}