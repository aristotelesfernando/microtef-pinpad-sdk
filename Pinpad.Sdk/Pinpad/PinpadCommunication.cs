using MicroPos.CrossPlatform;
using MicroPos.CrossPlatform.TypeCode;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Events;
using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Pinpad.Sdk.Model.Pinpad;

namespace Pinpad.Sdk
{
    /// <summary>
    /// Pinpad communication adapter.
    /// Responsible for sending and receiving commands to and from the pinpad.
    /// </summary>
    public sealed class PinpadCommunication : IPinpadCommunication
	{
		/* Members! */
		// Constants
		/// <summary>
		/// Timeout for Blocking messages
		/// </summary>
		public const int BLOCKING_TIMEOUT = 30000;
		/// <summary>
		/// Timeout to acknowledge a message
		/// </summary>
		internal const int ACKNOWLEDGE_TIMEOUT = 4000;
		/// <summary>
		/// Timeout to cancel a message
		/// </summary>
		internal const int CANCEL_TIMEOUT = 4000;
		/// <summary>
		/// Timeout for NonBlocking messages
		/// </summary>
		public const int NON_BLOCKING_TIMEOUT = 2000;
        /// <summary>
        /// Byte used to cancel the previous operation.
        /// </summary>
        internal const byte CANCEL_BYTE = 0x18;
        /// <summary>
        /// Pinpad response when a CAN is acknowledged.
        /// </summary>
        internal const byte EOT_BYTE = 0x04;
        /// <summary>
        /// Returned to the SPE from the pinpad, indicating an INVALID package. 
        /// In this case, the SPE must remand the package.
        /// </summary>
        internal const byte NOT_ACKNOWLEDGED_BYTE = 0x15;
        /// <summary>
        /// Returned to the SPE from the pinpad, indicating a valid package.
        /// </summary>
        internal const byte ACKNOWLEDGE_BYTE = 0x06;
        /// <summary>
        /// Null byte, used to verify whether the pinpad is connected/alive.
        /// </summary>
        internal const byte NULL_BYTE = 0x00;
        /// <summary>
        /// Number of times to try to remand a package in case of failure.
        /// </summary>
        internal const short TIMES_TO_REMAND_PACKAGE_ON_FAILURE = 3;

		// Public members
		/// <summary>
		/// Connection with Pinpad device
		/// </summary>
		public IPinpadConnection Connection { get; private set; }
		/// <summary>
		/// Last request string sent to the Pinpad
		/// </summary>
		public string LastSentRequest { get; private set; }
		/// <summary>
		/// Last response string received from the Pinpad
		/// </summary>
		public string LastReceivedResponse { get; private set; }
        /// <summary>
        /// Serial port in which the pinpad is attached.
        /// </summary>
		public string ConnectionName { get { return this.Connection.ConnectionName; } }

		// Private members
        /// <summary>
        /// Whether the current request was canceled.
        /// </summary>
		private bool _requestCanceled { get; set; }

		// Event handlers:
		/// <summary>
		/// Event for when the Pinpad receives a notification
		/// </summary>
		public EventHandler<PinpadNotificationEventArgs> NotificationReceived { get; set; }

		// Constructor:
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinpadConnection">Connection with Pinpad device</param>
		public PinpadCommunication (IPinpadConnection pinpadConnection)
		{
			if (pinpadConnection == null)
			{
				throw new ArgumentNullException("pinpadConnection cannot be null.");
			}

			// To change pinpad comm timeout
			this.Connection = pinpadConnection;
			this.Connection.ReadTimeout = NON_BLOCKING_TIMEOUT;
			this.Connection.WriteTimeout = NON_BLOCKING_TIMEOUT;
		}

		/* Public methods */
		/// <summary>
		/// Sends an OPN to the pinpad and do not close it.
		/// </summary>
		/// <returns>If the OPN was received, that is, if a pinpad was detected.</returns>
		public bool OpenPinpadConnection ()
		{
			try
			{
				// Verifies if serial port is opened:
				if (this.Connection.IsOpen == false)
				{
                    this.OpenConnectionSafely();
				}
				
				// Closes pinpad connection:
				this.ClosePinpadConnection(string.Empty);

				// Open pinpad connection:
				OpnResponse response = this.SendRequestAndReceiveResponse<OpnResponse>(new OpnRequest());

				if (response != null && response.RSP_STAT.Value == AbecsResponseStatus.ST_OK)
				{
					return true;
				}
			}
			catch (IOException)
			{
				return false;
			}
			catch (PropertyParseException)
			{
				this.ReceiveResponse<GenericResponse>(new AbecsContext());
				return this.OpenPinpadConnection();
			}

			return false;
		}
        /// <summary>
        /// Sends a CLO to the pinpad and closes the connection.
        /// </summary>
        /// <param name="message">Message to be present on pinpad display after the connection is closed.</param>
        /// <returns>If was closed or not.</returns>
        public bool ClosePinpadConnection (string message)
		{
			try
			{
				// Create CLO request:
				CloRequest request = new CloRequest();
				request.CLO_MSG.Value = new Properties.SimpleMessage(message, Model.DisplayPaddingType.Left);

				// Gets it's reponse
				CloResponse response = this.SendRequestAndReceiveResponse<CloResponse>(request);

				if (response == null)
				{

					return false;
				}

				if (response.RSP_STAT.Value == AbecsResponseStatus.ST_OK
					|| (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK && response.RSP_STAT.Value == AbecsResponseStatus.ST_NOTOPEN))
				{
					return true;
				}
			}
			catch (PropertyParseException)
			{
				this.ReceiveResponse<GenericResponse>(new AbecsContext());
				return this.ClosePinpadConnection(message);
			}

			return false;
		}
		/// <summary>
		/// Checks if the connection with the Pinpad is alive. If the pinpad is disconnected,
        /// tries to connect again.
		/// </summary>
		/// <returns>True if the connection is alive.</returns>
		public bool Ping ()
		{
			// Closes the pinpad connetion:
			bool status = this.ClosePinpadConnection(string.Empty);

			// If could not close pinpad connection, then the connection has been lost:
			if (status == false) { return false; }
			
			status = this.OpenPinpadConnection();

			return status;
		}
        /// <summary>
        /// Was the request accepted by the Pinpad?
        /// Cancels the current request at the Pinpad.
        /// Should be called only once after the request was sent.
        /// </summary>
        /// <returns>True if accepted, false to resend.</returns>
        public bool CancelRequest ()
		{
			if (this._requestCanceled == true) { return true; }

			// Send CAN byte (byte to cancel previous command)
			this.Connection.WriteByte(CANCEL_BYTE);
			lock (this.Connection)
			{
				if (this._requestCanceled == false)
				{
                    // Sends a CAN request while the pinpad does not acknowledge the cancellation.
                    // That is, while the application does not receive the EOT byte.
                    this.Connection.ReadTimeout = PinpadCommunication.CANCEL_TIMEOUT;

					byte b;

                    do
                    {
                        try
                        {
                            b = this.Connection.ReadByte();
                        }
                        catch (TimeoutException)
                        {
                            return false;
                        }
                    } while (b != EOT_BYTE); // Wait for EOT

                    this._requestCanceled = true;
				}
			}
			return this._requestCanceled;
		}
        /// <summary>
        /// Sends a request, receives the response then verifies if the command name
        /// is not ERR and response code equals <see cref="AbecsResponseStatus.ST_OK"/>.
        /// </summary>
        /// <param name="request">Request to send.</param>
        /// <returns>True if the request was sent, response was received, command is 
        /// not ERR and response code equals to <see cref="AbecsResponseStatus.ST_OK"/></returns>
        public bool SendRequestAndVerifyResponseCode(object request)
        {
            BaseCommand castRequest = request as BaseCommand;

            lock (this.Connection)
            {
                if (this.SendRequest(castRequest) == true)
                {
                    return this.ReceiveResponseAndVerifyResponseCode(castRequest.CommandContext);
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Sends a request and receives the specified generic type T.
        /// </summary>
        /// <typeparam name="T"><see cref="BaseResponse"/>, the corresponding response to
        /// the request sent.</typeparam>
        /// <param name="request">Request to send.</param>
        /// <returns>The response or null in case of failure.</returns>
        public T SendRequestAndReceiveResponse<T>(object request)
            where T : new()
        {
            BaseCommand castRequest = request as BaseCommand;

            if (castRequest == null)
            {
                throw new InvalidOperationException("Request does not implement expected type.");
            }

            lock (this.Connection)
            {
                if (this.SendRequest(castRequest) == false)
                {
                    return default(T);
                }
                else
                {
                    return (T)this.ReceiveResponse<T>(castRequest.CommandContext);
                }
            }
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
		/// Receives a specific response from the Pinpad
		/// </summary>
		/// <typeparam name="T">PinpadBaseResponseController to use</typeparam>
		/// <returns>response object or null if cancelled</returns>
		internal T ReceiveResponse<T> (IContext context = null)
			where T : new()
		{
			T response = new T();
            BaseResponse castResponse = response as BaseResponse;

            if (castResponse == null)
            {
                throw new InvalidOperationException("Response does not implement expected type.");
            }

            if (castResponse is GenericResponse)
			{
				if (context == null) { throw new ArgumentNullException("Context cannot be null."); }
                castResponse.CommandContext = context;
			}

			string responseString;
			if (castResponse.IsBlockingCommand == true)
			{
				responseString = this.ReceiveResponseString(PinpadCommunication.BLOCKING_TIMEOUT, 
                    castResponse.CommandContext);
				Debug.WriteLine("Response (blocking): " + responseString);
			}
			else
			{
				responseString = this.ReceiveResponseString(PinpadCommunication.NON_BLOCKING_TIMEOUT,
                    castResponse.CommandContext);
				Debug.WriteLine("Response: " + responseString);
			}

			if (responseString == null)
			{
				Debug.WriteLine("Response: (null)");
				return default(T);
			}

            castResponse.CommandString = responseString;
			return (T)(object)castResponse;
		}
		/// <summary>
		/// Receives a response from a command from the Pinpad
		/// </summary>
		/// <param name="timeout">Timeout of the message</param>
		/// <param name="context">Pinpad context.</param>
		/// <returns>response string or null if cancelled</returns>
		internal string ReceiveResponseString (int timeout, IContext context)
		{
			lock (this.Connection)
			{
				this.Connection.ReadTimeout = timeout;
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
						if (this.NotificationReceived != null)
						{
							this.NotificationReceived(this, new PinpadNotificationEventArgs(notificationResponse.NTM_MSG.Value));
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
		internal Nullable<bool> WasRequestAccepted ()
		{
			this.Connection.ReadTimeout = PinpadCommunication.ACKNOWLEDGE_TIMEOUT;

			byte acknowledge;

			// Reads a byte while do not receiva an ACK or NAK.
			do
			{
				try
				{
					acknowledge = this.Connection.ReadByte();
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
        /// Open connection on <see cref="Connection"/> only if the connection
        /// was not opened before.
        /// </summary>
        private void OpenConnectionSafely()
        {
            // Search for a pinpad in the specified serial port:
            this.Connection = PinpadConnectionManager.GetPinpadConnection(this.ConnectionName);

            if (this.Connection.IsOpen == false)
            {
                // Open SerialPort connection:
                this.Connection.Open();
            }
        }
        /// <summary>
        /// Sends a command to pinpad (mid-level)!
        /// </summary>
        /// <param name="data"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        private bool InternalSendRequest (byte [] data, int counter = 0)
		{
			// Send data
			this.Connection.Write(data);

			Nullable<bool> requestAccepted = this.WasRequestAccepted();
			if (requestAccepted == true)
			{
				Debug.WriteLine(this.LastSentRequest + " was accepted.");

				this._requestCanceled = false;

				// Pinpad acknowdledged the request:_
				return true;
			}
			// Try to remand up to 3 times:
			else if (requestAccepted == false && counter < TIMES_TO_REMAND_PACKAGE_ON_FAILURE)
			{
				Debug.WriteLine(this.LastSentRequest + " was denied.");
				return InternalSendRequest(data, counter + 1);
			}
			// Failed to send:
			else
			{
				return false;
			}
		}
        /// <summary>
        /// Locks the connection, cancels the last request and sends the current
        /// request to the pinpad.
        /// </summary>
        /// <param name="request">Request to send.</param>
        /// <returns>Return whether the command was send successfuly or not.</returns>
		private bool InternalSendRequest (BaseCommand request)
		{
			Debug.WriteLine("Request: " + request.CommandString);

			List<byte> requestByteCollection = request.CommandContext.GetRequestBody(request);

			lock (this.Connection)
			{
                // TODO: PINPADWIFI! Cancel Request comentado

                // Cancel the previous request:
                //this.CancelRequest();

                ////// Saves the current request as last:
                //this.LastSentRequest = request.CommandString;

                // Send the request:
                return InternalSendRequest(requestByteCollection.ToArray());
			}
		}
        /// <summary>
        /// Receive command response as string.
        /// </summary>
        /// <param name="context">Command context, used to format the response accordingly to
        /// the context features.</param>
        /// <param name="counter">Number of times to try to read the response (timeout).</param>
        /// <returns>The response as string or null in case of nothing received.</returns>
		private string InternalReceiveResponseString(IContext context, int counter = 0)
        {
            byte b;

            // Response itself
            List<byte> responseByteCollection = new List<byte>();

            // Reads a byte (possible garbage) until the reading of a SYN (in case of a normal request) or 
            // an EOT (in case of a cancel request):

            do
            {
                // Reads one byte:
                b = this.Connection.ReadByte();

                if (context.HasToIncludeFirstByte == true)
                {
                    responseByteCollection.Add(b);
                }

            } while (b != context.StartByte && b != EOT_BYTE);

            // In case of EOT, the request was cancelled:
            if (b == EOT_BYTE)
            {
                this._requestCanceled = true;
                return null;
            }

            // In case of SYN:
            string command = null;

            // Iterates, reading bytes from I/O buffer (while an ETB if found):
            do
            {
                // Reads one byte:
                b = this.Connection.ReadByte();

                // If the byte read is an ETB (response from a CAN request):
                if (b == context.EndByte)
                {
                    command = CrossPlatformController.TextEncodingController.GetString(TextEncodingType.Ascii, responseByteCollection.ToArray());
                }

                // Adds the byte to the response:
                responseByteCollection.Add(b);
            } while (b != context.EndByte);

            // Get's the checksum DIRECTLY from the response:
            byte[] receivedChecksum = new byte[context.IntegrityCodeLength];

            for (int i = 0; i < context.IntegrityCodeLength; i++)
            {
                receivedChecksum[i] = this.Connection.ReadByte();
            }

            // Generates a checksum from the response body:
            byte[] generatedChecksum = context.GetIntegrityCode(responseByteCollection.ToArray());

            //TODO: PINPADWIFI! Inversão dos index. Implementar de forma digna ou receber ja da forma correta do POS.
            if (this.Connection.CommunicationType == CommunicationType.Wifi) {
                byte[] auxByte = new byte[context.IntegrityCodeLength];
                auxByte[0] = generatedChecksum[0];
                auxByte[1] = generatedChecksum[1];
                generatedChecksum[0] = auxByte[1];
                generatedChecksum[1] = auxByte[0];
            }

			// Verify if both checksums match:
			if (context.IsIntegrityCodeValid(receivedChecksum, generatedChecksum) == false)
			{
				// If has tries to remand the message:
				if (counter < TIMES_TO_REMAND_PACKAGE_ON_FAILURE)
				{
					// Send a NAK to the pinpad:
					this.Connection.WriteByte(NOT_ACKNOWLEDGED_BYTE); 

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
