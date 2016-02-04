using CrossPlatformBase;
using PinPadSDK.PinPad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Connection
{
    /// <summary>
    /// Responsible for manage a Pinpad connection.
    /// </summary>
    public class PinpadConnection : BasePinpadConnection
    {
        /* MEMBERS */
        /// <summary>
        /// Whether the connection is opened or not.
        /// This property determines wheter something can be sent to the device or not. Should be verified after every attempting to send data to the pinpad.
        /// </summary>
        public override bool IsOpen
        {
            get
            {
                if (this.LegacyPinpadConnection == null)
                {
                    return false;
                }

                return this.LegacyPinpadConnection.IsOpen;
            }
        }

        /* METHODS */
        /// <summary>
        /// Search in all available serial ports for a pinpad connection.
        /// </summary>
        /// <returns>The serial port name of pinpad if found, or null otherwise</returns>
        public static string SearchPinpadPort()
        {
            IPinPadConnection connection = CrossPlatformBase.CrossPlatformController.PinPadFinder.Find();

            if (connection == null) { return null; }
            connection.Close();

            return connection.ConnectionName;
        }
        /// <summary>
        /// Try connect to pinpad in the specified serial port.
        /// </summary>
        /// <param name="portName">The serial port name.</param>
        /// <exception cref="System.IO.IOException">The port is in an invalid state. - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort object were invalid.</exception>
        public override void Open(string portName)
        {
            this.LegacyPinpadConnection = PinPadConnectionManager.GetPinPadConnection(portName);
            
            if (this.LegacyPinpadConnection.IsOpen == false) 
            { 
                this.LegacyPinpadConnection.Open(); 
            }
        }
        /// <summary>
        /// Disconnect from the attached serial port.
        /// </summary>
        /// <param name="portName">The serial port name.</param>
        public override void Close(string portName)
        {
            // Verify if the connection is valid/open.
            if (this.IsOpen == true)
            {
                return;
            }

            this.LegacyPinpadConnection.Close();
        }
        /// <summary>
        /// Sends a symbolic stream of bytes, to verify if the connection is OK.
        /// </summary>
        /// <returns>Whether the connection is alive or not.</returns>
        public override bool Ping()
        {
            if (this.IsOpen == false) { return false; }

            try
            {
                PinPadFacade pinpadFacade = new PinPadFacade(this.LegacyPinpadConnection);

                return pinpadFacade.Communication.IsConnectionAlive();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
