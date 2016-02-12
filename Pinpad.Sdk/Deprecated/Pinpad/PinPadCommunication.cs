using CrossPlatformBase;
using PinPadSDK.Commands;
using PinPadSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Enums;
using System.Threading;

namespace PinPadSDK.PinPad 
{
    /// <summary>
    /// PinPad communication adapter
    /// </summary>
    public class PinPadCommunication 
	{
		/* Members! */
		// Constants
        const UInt16 CRC_MASK = 0x1021;
		/// <summary>
		/// Timeout to acknowledge a message
		/// </summary>
		public const int ACKNOWLEDGE_TIMEOUT = 2000;
		/// <summary>
		/// Timeout to cancel a message
		/// </summary>
		public const int CANCEL_TIMEOUT = 2000;
		/// <summary>
		/// Timeout for NonBlocking messages
		/// </summary>
		public const int NON_BLOCKING_TIMEOUT = 10000;
		/// <summary>
		/// Timeout for Blocking messages
		/// </summary>
		public const int BLOCKING_TIMEOUT = 20000;

		// Public members
        /// <summary>
        /// Owner Facade
        /// </summary>
        public PinPadFacade PinPad { get; private set; }
        /// <summary>
        /// Connection with PinPad device
        /// </summary>
        public IPinPadConnection PinPadConnection { get; private set; }
        /// <summary>
        /// Last request string sent to the PinPad
        /// </summary>
        public string LastSentRequest { get; private set; }
        /// <summary>
        /// Last response string received from the PinPad
        /// </summary>
        public string LastReceivedResponse { get; private set; }
		/// <summary>
		/// Stone version of the PinPad, 0 for no stone application or null for failure
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

		// Private members
		private Nullable<int> _stoneVersion;
        private bool _requestCancelled { get; set; }

		// Event handlers:
		/// <summary>
		/// Event for when the PinPad receives a notification
		/// </summary>
		public event EventHandler<PinPadNotificationEventArgs> OnNotification;

		// Constructor:
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">Owner PinPad Facade</param>
        /// <param name="pinPadConnection">Connection with PinPad device</param>
        public PinPadCommunication(PinPadFacade pinPad, IPinPadConnection pinPadConnection) 
		{
            this.PinPadConnection = pinPadConnection;
            this.PinPad = pinPad;

			// To change pinpad comm timeout
			this.PinPadConnection.ReadTimeout = ACKNOWLEDGE_TIMEOUT;
			this.PinPadConnection.WriteTimeout = ACKNOWLEDGE_TIMEOUT;
        }

		/* Methods */
		// Public methods
        /// <summary>
        /// Checks if the connection with the PinPad is alive
        /// </summary>
        /// <returns>true if the connection is alive</returns>
        public bool IsConnectionAlive() 
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
        /// Was the request accepted by the PinPad?
        /// Should be called only once after the request was sent
        /// </summary>
        /// <returns>true if accepted, false to resend</returns>
        /// <summary>
        /// Cancels the current request at the PinPad
        /// </summary>
        /// <returns>true if the request was cancelled</returns>
        public bool CancelRequest() 
		{
            if (this._requestCancelled == true) 
			{
                return true;
            }

			// Send CAN byte
            this.PinPadConnection.WriteByte(0x18); 
            lock (this.PinPadConnection) 
			{
                if (this._requestCancelled == false) 
				{
                    this.PinPadConnection.ReadTimeout = PinPadCommunication.CANCEL_TIMEOUT;

                    byte b;
                    do 
					{
                        try 
						{
                            b = this.PinPadConnection.ReadByte();
                        }
                        catch (TimeoutException) 
						{
                            return false;
                        }
                    } while (b != 0x04); //Wait for EOT
                    this._requestCancelled = true;
                }
            }
            return this._requestCancelled;
        }
        /// <summary>
        /// Sends a request to the PinPad
        /// </summary>
        /// <param name="request">request controller</param>
        /// <returns>Was the request successfully sent?</returns>
        public bool SendRequest(BaseCommand request) 
		{
            BaseStoneRequest stoneRequest = request as BaseStoneRequest;
            
			if (stoneRequest != null) 
			{
                if (this.StoneVersion.HasValue == false || this.StoneVersion == 0) 
				{
                    throw new StoneVersionMismatchException("Comando " + request.CommandName + " requer aplicação Stone");
                }
                else if (this.StoneVersion < stoneRequest.MinimumStoneVersion) 
				{
                    throw new StoneVersionMismatchException("Comando " + request.CommandName + " requer versão Stone " + stoneRequest.MinimumStoneVersion + " enquanto PinPad possui versão " + this.StoneVersion);
                }
            }

            return this.SendRequest(request.CommandString);
        }
        /// <summary>
        /// Sends a command request to the PinPad
        /// </summary>
        /// <param name="request">request string</param>
        /// <returns>Was the request successfully sent?</returns>
        public bool SendRequest(string request) 
		{
            try 
			{
                if (this.StoneVersion >= new SecRequest().MinimumStoneVersion) 
				{
                    SecRequest secureRequest = PinPadEncryption.Instance.WrapRequest(request);
                    
					string secureRequestString = secureRequest.CommandString;
                    
					return this.InternalSendRequest(secureRequestString);
                }
                else 
				{
                    return this.InternalSendRequest(request);
                }
            }
            catch (Exception ex) 
			{
                if (ex is IOException == false &&
                    ex is TimeoutException == false &&
                    ex is InvalidOperationException == false) 
				{

                    CrossPlatformController.SendMailController.SendReportMailThreaded("PinPadSDK: EXCEPTION AT IsConnectionAlive", ex.ToString());
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
        public bool SendRequestAndVerifyResponseCode(BaseCommand request) 
		{
            lock (this.PinPadConnection) 
			{
                if (this.SendRequest(request) == true) 
				{
                    return this.ReceiveResponseAndVerifyResponseCode();
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
        public bool ReceiveResponseAndVerifyResponseCode( ) 
		{
            GenericResponse response = this.ReceiveResponse<GenericResponse>();

            if (response == null) 
			{
                return false;
            }
            else if (response.CommandName == "ERR")
			{
                return false;
            }
            else if (response.RSP_STAT.Value != ResponseStatus.ST_OK) 
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
        /// <typeparam name="responseType">PinPadBaseResponseController to use</typeparam>
        /// <param name="request">Request to send</param>
        /// <returns>responseType or null on failure</returns>
        public responseType SendRequestAndReceiveResponse<responseType>(BaseCommand request)
            where responseType : BaseResponse, new() 
		{
            lock (this.PinPadConnection) 
			{
                if (this.SendRequest(request) == false) 
				{
                    return null;
                }
                else 
				{
                    return this.ReceiveResponse<responseType>();
                }
            }
        }
        /// <summary>
        /// Receives a specific response from the PinPad
        /// </summary>
        /// <typeparam name="responseType">PinPadBaseResponseController to use</typeparam>
        /// <returns>response object or null if cancelled</returns>
        public responseType ReceiveResponse<responseType>()
            where responseType : BaseResponse, new( ) 
		{
            responseType response = new responseType();

            string responseString;
            if (response.IsBlockingCommand == true) 
			{
                responseString = this.ReceiveResponseString(PinPadCommunication.BLOCKING_TIMEOUT);
            }
            else 
			{
                responseString = this.ReceiveResponseString(PinPadCommunication.NON_BLOCKING_TIMEOUT);
            }

            if (responseString == null) 
			{
                return null;
            }

            response.CommandString = responseString;
            return response;
        }
        /// <summary>
        /// Receives a response from a command from the PinPad
        /// </summary>
        /// <param name="timeout">Timeout of the message</param>
        /// <returns>response string or null if cancelled</returns>
        public string ReceiveResponseString(int timeout) 
		{
            lock (this.PinPadConnection) 
			{
                this.PinPadConnection.ReadTimeout = timeout;
                string responseString = null;
                
				try 
				{
                    responseString = InternalReceiveResponseString();
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

                        CrossPlatformController.SendMailController.SendReportMailThreaded("PinPadSDK: EXCEPTION AT PinPad.PinPadCommunication:ReceiveResponseString", ex.ToString());
                        throw;
                    }
                }

                string openedResponseString;
                if (this.IsSecureResponse(responseString) == true) 
				{
                    openedResponseString = this.TranslateSecureResponse(responseString);
                }
                else 
				{
                    openedResponseString = responseString;
                }

                if (string.IsNullOrEmpty(openedResponseString)) 
				{
                    return null;
                }

                GenericResponse response = new GenericResponse();
                response.CommandString = openedResponseString;

                NtmResponse notificationResponse = new NtmResponse();
                if (response.CommandName == notificationResponse.CommandName) 
				{
                    notificationResponse.CommandString = openedResponseString;
                    if (this.OnNotification != null) 
					{
                        this.OnNotification(this, new PinPadNotificationEventArgs(notificationResponse.NTM_MSG.Value));
                    }
                    
					return this.ReceiveResponseString(timeout);
                }

                return openedResponseString;
            }
        }
        /// <summary>
        /// Generates the Data checksum accordingly to the ABECS specification
        /// </summary>
        /// <param name="data">Message Bytes</param>
        /// <returns>Checksum</returns>
        public static byte [] GenerateChecksum(byte [] data) 
		{
            UInt16 wData, wCRC = 0;

            for (int i = 0; i < data.Length; i++) 
			{
                wData = (UInt16)(data[i] << 8);
                for (int d = 0; d < 8; d++, wData <<= 1) 
				{
                    if (((wCRC ^ wData) & 0x8000) != 0) 
					{
                        wCRC = (UInt16)((wCRC << 1) ^ CRC_MASK);
                    }
                    else 
					{
                        wCRC <<= 1;
                    }
                }
            }

            return BitConverter.GetBytes(wCRC).Reverse().ToArray();
        }

		// Protected methods
        protected Nullable<bool> WasRequestAccepted() 
		{
            this.PinPadConnection.ReadTimeout = PinPadCommunication.ACKNOWLEDGE_TIMEOUT;

            byte acknowledge;
            do 
			{
                try 
				{
                    acknowledge = this.PinPadConnection.ReadByte();
                }
                catch (TimeoutException) 
				{
                    return null;
                }
                if (acknowledge == 0x15) 
				{
					//Return NULL in case of a Negative Acknowledge
                    return false; 
                }
            } while (acknowledge != 0x06); //Otherwise iterate through possible garbage until an Acknowledge is found
            
			return true;
        }

		// Private methods
		/// <summary>
		/// Sends a command to pinpad (mid-level)!
		/// </summary>
		/// <param name="data"></param>
		/// <param name="counter"></param>
		/// <returns></returns>
		private bool InternalSendRequest(byte[] data, int counter = 0)
		{
			//Send data
			this.PinPadConnection.Write(data);

			Nullable<bool> requestAccepted = this.WasRequestAccepted();
			if (requestAccepted == true)
			{
				this._requestCancelled = false;

				// PinPad acknowdledged the request
				return true;
			}
			else if (requestAccepted == false && counter < 2)
			{
				//Try to resend up to 3 times
				return InternalSendRequest(data, counter + 1);
			}
			else
			{
				//Failed to send
				return false;
			}
		}
		private bool InternalStoneIsConnectionAlive()
		{
			try
			{
				this.PinPadConnection.ReadTimeout = PinPadCommunication.ACKNOWLEDGE_TIMEOUT;
				this.PinPadConnection.WriteByte(0x00); //Send a NUL byte
				byte b;
				do
				{
					b = this.PinPadConnection.ReadByte();
				}
				while (b != 0x00); //Wait for NUL byte reply
				return true;
			}
			catch (Exception ex)
			{
				if (ex is IOException == false &&
					ex is TimeoutException == false &&
					ex is InvalidOperationException == false)
				{

					CrossPlatformController.SendMailController.SendReportMailThreaded("PinPadSDK: EXCEPTION AT IsConnectionAlive", ex.ToString());
					throw;
				}
			}

			return false;
		}
		private bool InternalIsConnectionAlive()
		{
			try
			{
				OpnRequest request = new OpnRequest();
				if (this.InternalSendRequest(request.CommandString) == false)
				{
					return false;
				}
				OpnResponse response = this.ReceiveResponse<OpnResponse>();

				if (response == null)
				{
					return false;
				}
				else if (response.OPN_STONEVER.HasValue)
				{
					this._stoneVersion = response.OPN_STONEVER.Value;
				}
				else
				{
					this._stoneVersion = 0;
				}

				return true;
			}
			catch (Exception ex)
			{
				if (ex is IOException == false &&
					ex is TimeoutException == false &&
					ex is InvalidOperationException == false)
				{

					CrossPlatformController.SendMailController.SendReportMailThreaded("PinPadSDK: EXCEPTION AT IsConnectionAlive", ex.ToString());
					throw;
				}
			}

			return false;
		}
		private bool IsSecureResponse(string responseString)
		{
			if (String.IsNullOrEmpty(responseString))
			{
				return false;
			}

			GenericResponse response = new GenericResponse();
			response.CommandString = responseString;

			SecResponse secureResponse = new SecResponse();

			return response.CommandName == secureResponse.CommandName;
		}
		private bool InternalSendRequest(string request)
		{
			List<byte> requestByteCollection = new List<byte>(CrossPlatformController.TextEncodingController.GetBytes(TextEncoding.ASCII, request));

			//Add ETB
			requestByteCollection.Add(0x17);

			//Generate checksum
			requestByteCollection.AddRange(PinPadCommunication.GenerateChecksum(requestByteCollection.ToArray()));

			//Add SYN
			requestByteCollection.Insert(0, 0x16);

			lock (this.PinPadConnection)
			{
				this.CancelRequest();
				this.LastSentRequest = request;
				return InternalSendRequest(requestByteCollection.ToArray());
			}
		}
		private string TranslateSecureResponse(string secureResponseString)
		{
			SecResponse secureResponse = new SecResponse();
			secureResponse.CommandString = secureResponseString;

			string responseString = PinPadEncryption.Instance.UnwrapResponse(secureResponse);
			return responseString;
		}
		private string InternalReceiveResponseString(int counter = 0)
		{
			byte b;

			do
			{
				b = this.PinPadConnection.ReadByte();
			} while (b != 0x16 && b != 0x04); //Iterate through possible garbage until an SYNor EOT is found

			if (b == 0x04)
			{
				this._requestCancelled = true;
				return null; //In case of EOT the request was cancelled
			}

			string command = null;
			List<byte> responseByteCollection = new List<byte>();
			do
			{
				b = this.PinPadConnection.ReadByte();
				if (b == 0x17)
				{
					command = CrossPlatformController.TextEncodingController.GetString(TextEncoding.ASCII, responseByteCollection.ToArray());
				}
				responseByteCollection.Add(b);
			} while (b != 0x17); //Read response bytes until ETB is found

			byte[] receivedChecksum = new byte[] { this.PinPadConnection.ReadByte(), this.PinPadConnection.ReadByte() };
			byte[] generatedChecksum = PinPadCommunication.GenerateChecksum(responseByteCollection.ToArray());

			if (receivedChecksum[0] != generatedChecksum[0] ||
					receivedChecksum[1] != generatedChecksum[1])
			{
				if (counter < 3)
				{
					this.PinPadConnection.WriteByte(0x15); //Send a NAK so the PinPad resends the response
					return InternalReceiveResponseString(counter + 1);
				}
				else
				{
					this.LastReceivedResponse = null;
					throw new InvalidChecksumException(this.LastSentRequest, this.LastReceivedResponse);
				}
			}

			responseByteCollection.Remove(0x17);
			this.LastReceivedResponse = CrossPlatformController.TextEncodingController.GetString(TextEncoding.ASCII, responseByteCollection.ToArray());
			return this.LastReceivedResponse;
		}
    }
}
