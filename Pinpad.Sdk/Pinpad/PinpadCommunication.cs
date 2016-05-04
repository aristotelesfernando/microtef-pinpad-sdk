using MicroPos.CrossPlatform;
using MicroPos.CrossPlatform.TypeCode;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Events;
using Pinpad.Sdk.TypeCode;
using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Pinpad communication adapter
	/// </summary>
	public class PinpadCommunication
	{
		/* Members! */
		// Constants
		/// <summary>
		/// Timeout to acknowledge a message
		/// </summary>
		public const int ACKNOWLEDGE_TIMEOUT = 4000;
		/// <summary>
		/// Timeout to cancel a message
		/// </summary>
		public const int CANCEL_TIMEOUT = 4000;
		/// <summary>
		/// Timeout for NonBlocking messages
		/// </summary>
		public const int NON_BLOCKING_TIMEOUT = 2000;
		/// <summary>
		/// Timeout for Blocking messages
		/// </summary>
		public const int BLOCKING_TIMEOUT = 30000;
		/// <summary>
		/// Byte used to cancel the previous operation.
		/// </summary>
		public const byte CANCEL_BYTE = 0x18;
		/// <summary>
		/// Pinpad response when a CAN is acknowledged.
		/// </summary>
		public const byte EOT_BYTE = 0x04;
		/// <summary>
		/// Returned to the SPE from the pinpad, indicating an INVALID package. 
		/// In this case, the SPE must remand the package.
		/// </summary>
		public const byte NOT_ACKNOWLEDGED_BYTE = 0x15;
		/// <summary>
		/// Returned to the SPE from the pinpad, indicating a valid package.
		/// </summary>
		public const byte ACKNOWLEDGE_BYTE = 0x06;
		/// <summary>
		/// Null byte, used to verify whether the pinpad is connected/alive.
		/// </summary>
		public const byte NULL_BYTE = 0x00;
		/// <summary>
		/// Number of times to try to remand a package in case of failure.
		/// </summary>
		public const short TIMES_TO_REMAND_PACKAGE_ON_FAILURE = 3;

		// Public members
		/// <summary>
		/// Connection with Pinpad device
		/// </summary>
		public PinpadConnection PinpadConnection { get; private set; }
		/// <summary>
		/// Last request string sent to the Pinpad
		/// </summary>
		public string LastSentRequest { get; private set; }
		/// <summary>
		/// Last response string received from the Pinpad
		/// </summary>
		public string LastReceivedResponse { get; private set; }
		/// <summary>
		/// Stone version of the Pinpad, 0 for no stone application or null for failure
		/// </summary>
		public Nullable<int> StoneVersion
		{
			get
			{
				if (this._stoneVersion.HasValue == false)
				{
					this.InternalIsConnectionAlive();
				}

				return this._stoneVersion;
			}
		}
		public bool OpenPinpadConnection ()
		{
			OpnResponse response = this.SendRequestAndReceiveResponse<OpnResponse>(new OpnRequest());

			if (response != null && response.RSP_STAT.Value == AbecsResponseStatus.ST_OK)
			{
				return true;
			}

			return false;
		}

		// Private members
		private Nullable<int> _stoneVersion;
		private bool _requestCancelled { get; set; }

		// Event handlers:
		/// <summary>
		/// Event for when the Pinpad receives a notification
		/// </summary>
		public EventHandler<PinpadNotificationEventArgs> OnNotification { get; set; }

		// Constructor:
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinPad">Owner Pinpad Facade</param>
		/// <param name="pinpadConnection">Connection with Pinpad device</param>
		public PinpadCommunication (PinpadConnection pinpadConnection)
		{
			if (pinpadConnection == null)
			{
				throw new ArgumentNullException("pinpadConnection cannot be null.");
			}

			this.PinpadConnection = pinpadConnection;

			// To change pinpad comm timeout
			this.PinpadConnection.Connection.ReadTimeout = NON_BLOCKING_TIMEOUT;
			this.PinpadConnection.Connection.WriteTimeout = NON_BLOCKING_TIMEOUT;
		}

		/* Public methods */
		/// <summary>
		/// Checks if the connection with the Pinpad is alive
		/// </summary>
		/// <returns>true if the connection is alive</returns>
		public bool IsConnectionAlive ()
		{
			if (this._stoneVersion.HasValue == true && this._stoneVersion.Value > 0)
			{
				return InternalStoneIsConnectionAlive();
			}
			else
			{
				return InternalIsConnectionAlive();
			}
		}
		/// <summary>
		/// Was the request accepted by the Pinpad?
		/// Should be called only once after the request was sent
		/// </summary>
		/// <returns>true if accepted, false to resend</returns>
		/// <summary>
		/// Cancels the current request at the Pinpad
		/// </summary>
		/// <returns>true if the request was cancelled</returns>
		public bool CancelRequest ()
		{
			if (this._requestCancelled == true) { return true; }

			// Send CAN byte (byte to cancel previous command)
			this.PinpadConnection.Connection.WriteByte(CANCEL_BYTE);
			lock (this.PinpadConnection)
			{
				if (this._requestCancelled == false)
				{
					this.PinpadConnection.Connection.ReadTimeout = PinpadCommunication.CANCEL_TIMEOUT;

					byte b;

					// Sends a CAN request while the pinpad does not acknowledge the cancellation.
					// That is, while the application does not receive the EOT byte.
					do
					{
						try
						{
							b = this.PinpadConnection.Connection.ReadByte();
						}
						catch (TimeoutException)
						{
							return false;
						}
					} while (b != EOT_BYTE); // Wait for EOT

					this._requestCancelled = true;
				}
			}
			return this._requestCancelled;
		}
		
		/* Used internally */
		/// <summary>
		/// Sends a request to the Pinpad
		/// </summary>
		/// <param name="request">request controller</param>
		/// <returns>Was the request successfully sent?</returns>
		internal bool SendRequest (BaseCommand request)
		{
            try
            {
					return this.InternalSendRequest(request);
			}
			catch (Exception ex)
			{
				if (ex is IOException == false &&
					ex is TimeoutException == false &&
					ex is InvalidOperationException == false)
				{
					//CrossPlatformController.SendMailController.SendReportMailThreaded("PinpadSDK: EXCEPTION AT IsConnectionAlive", ex.ToString());
					throw;
				}
				else
				{
					return false;
				}
			}
		}
		/// <summary>
		/// Sends a request, receives the response then verifies if the command is not ERR and response code equals ST_OK
		/// </summary>
		/// <param name="request">Request to send</param>
		/// <returns>true if the request was sent, response was received, command is not ERR and response code equals ST_OK</returns>
		internal bool SendRequestAndVerifyResponseCode (BaseCommand request)
		{
			lock (this.PinpadConnection)
			{
				if (this.SendRequest(request) == true)
				{
					return this.ReceiveResponseAndVerifyResponseCode(request.CommandContext);
				}
				else
				{
					return false;
				}
			}
		}
		/// <summary>
		/// Receives a generic response then verifies if the command is not ERR and response code equals ST_OK
		/// </summary>
		/// <returns>true if the response was received, command is not ERR and response code equals ST_OK</returns>
		internal bool ReceiveResponseAndVerifyResponseCode (IContext context)
		{
			GenericResponse response = this.ReceiveResponse<GenericResponse>(context);

			if (response == null)
			{
				return false;
			}
			else if (response.CommandName == "ERR")
			{
				return false;
			}
			else if (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		/// <summary>
		/// Sends a Request and Receives the specified responseType
		/// </summary>
		/// <typeparam name="responseType">PinpadBaseResponseController to use</typeparam>
		/// <param name="request">Request to send</param>
		/// <returns>responseType or null on failure</returns>
		internal responseType SendRequestAndReceiveResponse<responseType> (BaseCommand request)
			where responseType : BaseResponse, new()
		{
			lock (this.PinpadConnection)
			{
				if (this.SendRequest(request) == false)
				{
					return null;
				}
				else
				{
					return this.ReceiveResponse<responseType>(request.CommandContext);
				}
			}
		}
		/// <summary>
		/// Receives a specific response from the Pinpad
		/// </summary>
		/// <typeparam name="responseType">PinpadBaseResponseController to use</typeparam>
		/// <returns>response object or null if cancelled</returns>
		internal responseType ReceiveResponse<responseType> (IContext context = null)
			where responseType : BaseResponse, new()
		{
			responseType response = new responseType();

			if (response is GenericResponse)
			{
				if (context == null) { throw new ArgumentNullException("Context cannot be null."); }
				response.CommandContext = context;
			}

			string responseString;
			if (response.IsBlockingCommand == true)
			{
				responseString = this.ReceiveResponseString(PinpadCommunication.BLOCKING_TIMEOUT, response.CommandContext);
				Debug.WriteLine("Response (blocking): " + responseString);

			}
			else
			{
				responseString = this.ReceiveResponseString(PinpadCommunication.NON_BLOCKING_TIMEOUT, response.CommandContext);
				Debug.WriteLine("Response: " + responseString);
			}

			if (responseString == null)
			{
				Debug.WriteLine("Response: (null)");
				return null;
			}


			response.CommandString = responseString;
			return response;
		}
		/// <summary>
		/// Receives a response from a command from the Pinpad
		/// </summary>
		/// <param name="timeout">Timeout of the message</param>
		/// <returns>response string or null if cancelled</returns>
		internal string ReceiveResponseString (int timeout, IContext context)
		{
			lock (this.PinpadConnection)
			{
				this.PinpadConnection.Connection.ReadTimeout = timeout;
				string responseString = null;

				try
				{
					responseString = InternalReceiveResponseString(context);
				}
				catch (Exception ex)
				{
					if (ex is InvalidChecksumException)
					{
						throw;
					}
					else if (ex is IOException == false &&
						ex is TimeoutException == false &&
						ex is InvalidOperationException == false)
					{

						//CrossPlatformController.SendMailController.SendReportMailThreaded("PinpadSDK: EXCEPTION AT Pinpad.PinpadCommunication:ReceiveResponseString", ex.ToString());
						throw;
					}
				}

				if (string.IsNullOrEmpty(responseString) == true)
				{
					return null;
				}

				string openedResponseString  = responseString;

				if (context is AbecsContext)
				{
					GenericResponse response = new GenericResponse(context);
					response.CommandString = openedResponseString;

					NtmResponse notificationResponse = new NtmResponse();
					if (response.CommandName == notificationResponse.CommandName)
					{
						notificationResponse.CommandString = openedResponseString;
						if (this.OnNotification != null)
						{
							this.OnNotification(this, new PinpadNotificationEventArgs(notificationResponse.NTM_MSG.Value));
						}

						return this.ReceiveResponseString(timeout, response.CommandContext);
					}
				}

				return openedResponseString;
			}
		}
		/// <summary>
		/// Verifies if the pinpad has acknowledged the request.
		/// </summary>
		/// <returns>True if the pinpad has acknowledged the request; false otherwise and null if error.</returns>
		protected Nullable<bool> WasRequestAccepted ()
		{
			this.PinpadConnection.Connection.ReadTimeout = PinpadCommunication.ACKNOWLEDGE_TIMEOUT;

			byte acknowledge;

			// Reads a byte while do not receiva an ACK or NAK.
			do
			{
				try
				{
					acknowledge = this.PinpadConnection.Connection.ReadByte();
				}
				catch (TimeoutException)
				{
					return null;
				}

				// Return NULL in case of a Negative Acknowledge
				if (acknowledge == NOT_ACKNOWLEDGED_BYTE)
				{
					return false;
				}
			} while (acknowledge != ACKNOWLEDGE_BYTE);

			return true;
		}

		// Private methods
		/// <summary>
		/// Sends a command to pinpad (mid-level)!
		/// </summary>
		/// <param name="data"></param>
		/// <param name="counter"></param>
		/// <returns></returns>
		private bool InternalSendRequest (byte [] data, int counter = 0)
		{
			// Send data
			this.PinpadConnection.Connection.Write(data);

			Nullable<bool> requestAccepted = this.WasRequestAccepted();
			if (requestAccepted == true)
			{
				this._requestCancelled = false;

				// Pinpad acknowdledged the request:_
				return true;
			}
			// Try to remand up to 3 times:
			else if (requestAccepted == false && counter < TIMES_TO_REMAND_PACKAGE_ON_FAILURE)
			{
				return InternalSendRequest(data, counter + 1);
			}
			// Failed to send:
			else
			{
				return false;
			}
		}
		private bool InternalStoneIsConnectionAlive ()
		{
			try
			{
				// Set operation timeout:
				this.PinpadConnection.Connection.ReadTimeout = PinpadCommunication.ACKNOWLEDGE_TIMEOUT;

				// Send a null byte (if the pinpad is connected and alive, it'll return a null byte):
				this.PinpadConnection.Connection.WriteByte(NULL_BYTE);

				byte b;

				// Reads a byte while a null byte is not received:
				do
				{
					b = this.PinpadConnection.Connection.ReadByte();
				}
				while (b != NULL_BYTE);

				return true;
			}
			catch (Exception ex)
			{
				if (ex is IOException == false &&
					ex is TimeoutException == false &&
					ex is InvalidOperationException == false)
				{

					//CrossPlatformController.SendMailController.SendReportMailThreaded("PinpadSDK: EXCEPTION AT IsConnectionAlive", ex.ToString());
					throw;
				}
			}

			return false;
		}
		private bool InternalIsConnectionAlive ()
		{
			try
			{
				OpnRequest request = new OpnRequest();

				// Tries to send an OPN request.
				// Verifies if the request was sent an replied:
				if (this.InternalSendRequest(request) == false)
				{
					return false;
				}

				// If there was any reply, read it:
				OpnResponse response = this.ReceiveResponse<OpnResponse>();

				// If the response is null, returns failure:
				if (response == null) { return false; }

				// If there is Stone Version in the reply:
				else if (response.OPN_STONEVER.HasValue == true)
				{
					this._stoneVersion = response.OPN_STONEVER.Value;
				}
				else { this._stoneVersion = 0; }

				return true;
			}
			catch (Exception ex)
			{
				if (ex is IOException == false &&
					ex is TimeoutException == false &&
					ex is InvalidOperationException == false)
				{
					//CrossPlatformController.SendMailController.SendReportMailThreaded("PinpadSDK: EXCEPTION AT IsConnectionAlive", ex.ToString());
					throw;
				}
			}

			return false;
		}
		private bool InternalSendRequest (BaseCommand request)
		{
			Debug.WriteLine("Request: " + request.CommandString);

			List<byte> requestByteCollection = request.CommandContext.GetRequestBody(request);

			lock (this.PinpadConnection)
			{
				// Cancel the previous request:
				this.CancelRequest();

				// Saves the current request as last:
				this.LastSentRequest = request.CommandString;

				// Send the request:
				return InternalSendRequest(requestByteCollection.ToArray());
			}
		}
		private string InternalReceiveResponseString (IContext context, int counter = 0)
		{
			byte b;

			// Response itself
			List<byte> responseByteCollection = new List<byte>();

			// Reads a byte (possible garbage) until the reading of a SYN (in case of a normal request) or 
			// an EOT (in case of a cancel request):
			do
			{
				// Reads one byte:
				b = this.PinpadConnection.Connection.ReadByte();

				if (context.HasToIncludeFirstByte == true)
				{
					responseByteCollection.Add(b);
				}

			} while (b != context.StartByte && b != EOT_BYTE);

			// In case of EOT, the request was cancelled:
			if (b == EOT_BYTE)
			{
				this._requestCancelled = true;
				return null;
			}

			// In case of SYN:
			string command = null;

			// Iterates, reading bytes from I/O buffer (while an ETB if found):
			do
			{
				// Reads one byte:
				b = this.PinpadConnection.Connection.ReadByte();

				// If the byte read is an ETB (response from a CAN request):
				if (b == context.EndByte)
				{
					command = CrossPlatformController.TextEncodingController.GetString(TextEncodingType.Ascii, responseByteCollection.ToArray());
				}

				// Adds the byte to the response:
				responseByteCollection.Add(b);
			} while (b != context.EndByte);

			// Get's the checksum DIRECTLY from the response:
			byte [] receivedChecksum = new byte [context.IntegrityCodeLength];

			for (int i = 0; i < context.IntegrityCodeLength; i++)
			{
				receivedChecksum [i] = this.PinpadConnection.Connection.ReadByte();
			}

			// Generates a checksum from the response body:
			byte [] generatedChecksum = context.GetIntegrityCode(responseByteCollection.ToArray());

			// Verify if both checksums match:
			if (context.IsIntegrityCodeValid(receivedChecksum, generatedChecksum) == false)
			{
				// If has tries to remand the message:
				if (counter < TIMES_TO_REMAND_PACKAGE_ON_FAILURE)
				{
					// Send a NAK to the pinpad:
					this.PinpadConnection.Connection.WriteByte(NOT_ACKNOWLEDGED_BYTE); 

					// Thies to read the message again:
					return InternalReceiveResponseString(context, counter + 1);
				}

				// If number of tries has exceed:
				else
				{
					this.LastReceivedResponse = null;
					throw new InvalidChecksumException(this.LastSentRequest, this.LastReceivedResponse);
				}
			}

			responseByteCollection.Remove(context.EndByte);
			context.FormatResponse(responseByteCollection);
			this.LastReceivedResponse = CrossPlatformController.TextEncodingController.GetString(TextEncodingType.Ascii, responseByteCollection.ToArray());
			return this.LastReceivedResponse;
		}
	}
}
