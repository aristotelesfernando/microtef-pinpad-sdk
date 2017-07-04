using Microtef.CrossPlatform;
using Pinpad.Sdk.Model.Constants;

namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Contract for a class to establish logic connection to a pinpad,
    /// send and receive commands to and from the pinpad.
    /// </summary>
    public interface IPinpadCommunication
    {
        /// <summary>
        /// Pinpad connection at the link level.
        /// </summary>
        IPinpadConnection Connection { get; }
        /// <summary>
        /// Port name in which the pinpad is connected.
        /// </summary>
        string ConnectionName { get; }
        /// <summary>
        /// Sets time interval for sending and receiving messages.
        /// </summary>
        /// <param name="writeTimeout">Time interval to wait for a message to be sent.Time in milliseconds and must be greater than 2 seconds to be changed</param>
        /// <param name="readTimeout">Time interval to wait for a message to be received.Time in milliseconds  and must be greater than 2 seconds to be changed</param>
        void SetTimeout(int writeTimeout = Timeout.NON_BLOCKING_TIMEOUT, int readTimeout = Timeout.NON_BLOCKING_TIMEOUT);
        /// <summary>
        /// Open logic connection to the pinpad.
        /// </summary>
        /// <returns></returns>
        bool OpenPinpadConnection();
        /// <summary>
        /// Close logic connection, if it is connected.
        /// </summary>
        /// <param name="message">Message to display after closing connection.</param>
        /// <returns>Whether the connection was closed or not.</returns>
        bool ClosePinpadConnection(string message);
        /// <summary>
		/// Checks if the connection with the Pinpad is alive. If the pinpad is disconnected,
        /// tries to connect again.
		/// </summary>
		/// <returns>True if the connection is alive.</returns>
        bool Ping();
        /// <summary>
        /// Was the request accepted by the Pinpad?
        /// Cancels the current request at the Pinpad.
        /// Should be called only once after the request was sent.
        /// </summary>
        /// <returns>True if accepted, false to resend.</returns>
        bool CancelRequest();
        /// <summary>
        /// Sends a request and receives the specified generic type T.
        /// </summary>
        /// <typeparam name="T">The corresponding response to
        /// the request sent.</typeparam>
        /// <param name="request">Request to send.</param>
        /// <returns>The response or null in case of failure.</returns>
        T SendRequestAndReceiveResponse<T>(object request)
            where T : new();
        /// <summary>
        /// Sends a request, receives the response then verifies if the command name
        /// is not ERR and response code corresponds to success.
        /// </summary>
        /// <param name="request">Request to send.</param>
        /// <returns>True if the request was sent, response was received, command is 
        /// not ERR and response code corresponds to success.</returns>
        bool SendRequestAndVerifyResponseCode(object request);
    }
}
