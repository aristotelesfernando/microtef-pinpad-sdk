using Pinpad.Sdk.Connection;
using Pinpad.Sdk.Display.Mapper;
using Pinpad.Sdk.Model.TypeCode;
using PinPadSDK.Enums;
using PinPadSDK.PinPad;
using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Display
{
    /// <summary>
    /// Responsible for presenting messages in a pinpad device.
    /// </summary>
    public class PinpadDisplay : IPinpadDisplay
    {
        public const int DISPLAY_LINE_WIDTH = 16;

        /// <summary>
        /// Pinpad connection referencing the current pinpad, which will receive all information send by this class.
        /// </summary>
        private BasePinpadConnection pinpadConnection;
        /// <summary>
        /// Constructor. Sets pinpad communication, mandatory to show a message through the pinpad device.
        /// </summary>
        /// <param name="pinpadConnection">Pinpad connection, representing the pinpad that this class will reference.</param>
        /// <exception cref="System.ArgumentNullException">In case of nullable parameter.</exception>
        public PinpadDisplay(BasePinpadConnection pinpadConnection)
        {
            if (pinpadConnection == null)
            {
                throw new ArgumentNullException("pinpadConnection");
            }

            this.pinpadConnection = pinpadConnection;
        }
        /// <summary>
        /// Show message on pinpad screen.
        /// </summary>
        /// <param name="firstLine">The first line of the message, shown at the first screen line. Must have 16 characters or less.</param>
        /// <param name="secondLine">The second line of the message, shown at the second screen line. Must have 16 characters or less.</param>
        /// <param name="paddingType">At what alignment the message is present. It default value is left alignment.</param>
        /// <returns>Whether the message could be shown with success or not.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">This exception is thrown only if one (or both) of the messages exceed the limit of 16 characters.</exception>
        public bool ShowMessage(string firstLine, string secondLine = null, DisplayPaddingType paddingType = DisplayPaddingType.Left)
        {
            if (firstLine != null && firstLine.Length > 16)     { firstLine = firstLine.Substring(0, 16); }
            if (secondLine != null && secondLine.Length > 16)   { secondLine = secondLine.Substring(0, 16); }

            try
            {
                PinPadFacade pinpadFacade = new PinPadFacade(this.pinpadConnection.LegacyPinpadConnection);
                
                SimpleMessage message = new SimpleMessage(firstLine, secondLine, DisplayPaddingMapper.MapPaddingType(paddingType));

                pinpadFacade.Display.DisplayMessage(message);
                
                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
