using MicroPos.CrossPlatform;
using Pinpad.Sdk.Commands;

namespace Pinpad.Sdk
{
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

			if (conn == null) { return null; }

			return new PinpadConnection(conn);
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

			if (this.Connection.IsOpen == false)
			{
				this.Connection.Open();
			}
		}
		/// <summary>
		/// Disconnect from the attached serial port.
		/// </summary>
		/// <param name="portName">The serial port name.</param>
		public void Close (string portName)
		{
			// Verify if the connection is valid/open:
			if (this.IsOpen == true) { return; }

			// Closes the connection:
			this.Connection.Close();
		}
		/// <summary>
		/// Sends a symbolic stream of bytes, to verify if the connection is OK.
		/// </summary>
		/// <returns>Whether the connection is alive or not.</returns>
		public bool Ping ()
		{
			if (this.IsOpen == false) { return false; }

			return this.IsConnectionAlive();
		}

		// Internals
		/// <summary>
		/// Sends an OPN to verify if the connection is alive.
		/// </summary>
		/// <returns></returns>
		internal bool IsConnectionAlive ()
		{
			PinpadCommunication comm = new PinpadCommunication(this);
			return comm.SendRequestAndVerifyResponseCode(new OpnRequest());
		}
	}
}
