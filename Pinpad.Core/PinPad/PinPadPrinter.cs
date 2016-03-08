﻿using Pinpad.Core.Commands;
using Pinpad.Core.TypeCode;
using Pinpad.Core.Utilities;
using Pinpad.Sdk.Model;
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
		/// Pinpad communication provider.
		/// </summary>
		private PinpadCommunication communication;
		/// <summary>
		/// Pinpad information provider.
		/// </summary>
		private PinpadInfos informations;
		/// <summary>
		/// If pinpad has printer.
		/// </summary>
		private Nullable<bool> ispPinterSupported;
		/// <summary>
		/// If pinpad has printer.
		/// </summary>
		public bool PrinterSupported
		{
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

				else { return false; }
			}
		}

		// Constructor
		/// <summary>
		/// Basic constructor.
		/// </summary>
		/// <param name="communication">Pinpad communication provider.</param>
		/// <param name="informations">Pinpad information provider.</param>
		public PinpadPrinter(PinpadCommunication communication, PinpadInfos informations) 
		{
			this.communication = communication;
			this.informations = informations;
		}

		// Methods
		private Nullable<bool> IsPrinterSupported()
		{
			if (this.communication.StoneVersion == null) { return null; }

			else if (this.communication.StoneVersion < new PrtRequest().MinimumStoneVersion)
			{
				return false;
			}

			// Pinpad models that contain printer should be specified here:
			switch (this.informations.Model.Trim())
			{
				case "D210":
					return true;
				default:
					return false;
			}
		}
		/// <summary>
		/// Prepares the Printer to receive data.
		/// </summary>
		/// <returns>True on success.</returns>
		public bool Begin() 
		{
			PrtRequest request = new PrtRequest( );
			request.BeginData = new PrtBeginRequestData();

			// Send a request to the pinpad, indicating that a pressing is about to begin:
			return this.SendPrt(request);
		}
		/// <summary>
		/// Prints the data at the buffer.
		/// </summary>
		/// <returns>True on success.</returns>
		public bool End()
		{
			PrtRequest request = new PrtRequest();
			request.EndData = new PrtEndRequestData();

			// Sends a request to the pinpad, indicating that there is no more content to be pressed:
			return this.SendPrt(request);
		}
		/// <summary>
		/// Appends a string to the print buffer.
		/// </summary>
		/// <param name="message">String to append.</param>
		/// <param name="size">Size of the message.</param>
		/// <param name="alignment">Alignment of the message.</param>
		/// <returns>True on success.</returns>
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
		/// Appends a image to the print buffer.
		/// </summary>
		/// <param name="fileName">Image file at the Pinpad.</param>
		/// <param name="horizontal">Left padding in pixels.</param>
		/// <returns>True on success.</returns>
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
		/// Sets the vertical offset for the next append.
		/// </summary>
		/// <param name="steps">Vertical offset for the next append, can be negative.</param>
		/// <returns>True on success.</returns>
		public bool Step(int steps) 
		{
			PrtRequest request = new PrtRequest();

			PrtStepRequestData stepData = new PrtStepRequestData();
			stepData.PRT_STEPS.Value = steps;

			request.StepData = stepData;

			return this.SendPrt(request);
		}
		/// <summary>
		/// Sends the request to print.
		/// </summary>
		/// <param name="request">Content to be printed.</param>
		/// <returns>If the content was printed or not.</returns>
		private bool SendPrt(PrtRequest request) 
		{
			PrtResponse response = this.communication.SendRequestAndReceiveResponse<PrtResponse>(request);

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) 
			{
				return false;
			}

			if (response.BeginData != null) 
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