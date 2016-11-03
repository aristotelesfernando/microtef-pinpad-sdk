using MicroPos.CrossPlatform;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model.Exceptions;
using System.IO;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Pinpad connection handler.
	/// </summary>
	public class PinpadConnection
	{
		// Members
		/// <summary>
		/// Actually connection, depending on the platform.
		/// </summary>
		public IPinpadConnection Connection;

		/// <summary>
		/// Whether the connection is opened or not.
		/// This property determines wheter something can be sent to the device or not. Should be verified after every attempting to send data to the pinpad.
		/// </summary>
		public bool IsOpen
		{
			get
			{
				if (this.Connection == null) { return false; }

				return this.Connection.IsOpen;
			}
		}

		// Constructor
		/// <summary>
		/// Creates the object based on the <see cref="IPinpadConnection">connection provided</see> by the platform (Desktop, UWP...).
		/// </summary>
		/// <param name="connection">Connection provided by the platform (Desktop, UWP...).</param>
		public PinpadConnection (IPinpadConnection connection)
		{
			this.Connection = connection;
		}

		// Static method
		/// <summary>
		/// Finds one pinpad and connects to it (connection is not closed).
		/// </summary>
		/// <returns>Returns the first pinpad found.</returns>
		public static PinpadConnection GetFirst ()
		{
			IPinpadConnection conn = CrossPlatformController.PinpadFinder.Find();

			if (conn == null)
			{
				throw new PinpadNotFoundException();
			}

			return new PinpadConnection(conn);
		}
		/// <summary>
		/// Search for a pinpad attached to <param name="portName"/>.
		/// </summary>
		/// <returns>Returns the pinpad found.</returns>
		/// <exception cref="PinpadNotFoundException">If none pinpad were found at the port specified.</exception>
		public static PinpadConnection GetAt (string portName)
		{
            IPinpadConnection connection = null;

            try
			{
                // Open connection with serial port:
                connection = CrossPlatformController.PinpadFinder.Find(portName);
			}
			catch (IOException)
			{
				throw new PinpadNotFoundException(string.Format("None pinpad found at {0}.", portName), portName);
			}

			return new PinpadConnection(connection);
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

		// Methods
		/// <summary>
		/// Try connect to pinpad in the specified serial port.
		/// </summary>
		/// <param name="portName">The serial port name.</param>
		/// <exception cref="System.IO.IOException">The port is in an invalid state. - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort object were invalid.</exception>
		public void Open (string portName)
		{
			// Search for a pinpad in the specified serial port:
			this.Connection = PinpadConnectionManager.GetPinpadConnection(portName);

			if (this.IsOpen == false)
			{
				// Open SerialPort connection:
				this.Connection.Open();
			}
		}
		/// <summary>
		/// Disconnect from the attached serial port.
		/// </summary>
		public void Close ()
		{
			// Verify if the connection is valid/open:
			if (this.IsOpen == true) { return; }

			// Closes SerialPort connection:
			this.Connection.Close();
		}
	}
}
