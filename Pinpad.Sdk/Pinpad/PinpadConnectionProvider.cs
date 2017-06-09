using MicroPos.CrossPlatform;
using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Sdk.Model.Pinpad;
using System.IO;
using System;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Pinpad connection handler.
	/// </summary>
	public abstract class PinpadConnectionProvider
	{
        // Static method
        /// <summary>
        /// Finds one pinpad and connects to it (connection is not closed).
        /// </summary>
        /// <param name="enableWifi">Whether Wi-Fi pinpads shall be searched.</param>
        /// <param name="stoneCode">Stone Code.</param>
        /// <returns>Returns the first pinpad found.</returns>
        public static IPinpadConnection GetFirst (bool enableWifi = false, string stoneCode = "StoneCode")
		{
			IPinpadConnection conn = CrossPlatformController.PinpadFinder.Find(enableWifi, stoneCode);

			if (conn == null)
			{
				throw new PinpadNotFoundException();
			}

			return conn;
		}
        /// <summary>
        /// Search for a pinpad attached to <param name="portName"/>.
        /// </summary>
        /// <param name="stoneCode">Stone Code.</param>
        /// <returns>Returns the pinpad found.</returns>
        /// <exception cref="PinpadNotFoundException">If none pinpad were found at the port specified.</exception>
        public static IPinpadConnection GetAt (string portName, string stoneCode = "StoneCode")
		{
            IPinpadConnection connection = null;

            try
			{
                // Open connection with serial port:
                connection = CrossPlatformController.PinpadFinder.Find(portName, stoneCode);
			}
			catch (IOException)
			{
				throw new PinpadNotFoundException(string.Format("None pinpad found at {0}.", portName), portName);
			}

			return connection;
		}
		/// <summary>
		/// Search in all available serial ports for a pinpad connection.
		/// </summary>
		/// <returns>The serial port name of pinpad if found, or null otherwise.</returns>
		public static string SearchPinpadPort ()
		{
			// Search for a pinpad:
			IPinpadConnection connection = CrossPlatformController.PinpadFinder.Find();

			// If connection is null, then none pinpad was found.
			if (connection == null) { return null; }

			// Otherwise, closes the connection (because the method must only return the pinpad port, not a connection):
			connection.Close();

			return connection.ConnectionName;
		}
	}
}
