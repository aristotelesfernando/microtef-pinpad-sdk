using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Model;
using System;

namespace Pinpad.Sdk 
{
	/// <summary>
	/// Pinpad display API.
	/// </summary>
	public sealed class PinpadDisplay : IPinpadDisplay
	{
		// Constants
		/// <summary>
		/// Maximum number of characters that fit in one line of pinpad display.
		/// </summary>
		public const int DISPLAY_LINE_WIDTH = 16;

		// Members
		/// <summary>
		/// Pinpad communication.
		/// </summary>
		private PinpadCommunication communication;

		// Constructor
		/// <summary>
		/// Default constructor. Creates pinpad display facade.
		/// </summary>
		/// <param name="communication"></param>
		public PinpadDisplay (PinpadCommunication communication) 
		{
			if (communication == null)
			{
				throw new ArgumentNullException("PinpadCommunication cannot be null.");
			}

			this.communication = communication;
		}

		// Methods
		/// <summary>
		/// Displays a message in the PinPad display with the default, safe, method
		/// </summary>
		/// <param name="message">Message to display</param>
		/// <returns>true if message is displayed in the PinPad</returns>
		public bool DisplayMessage(SimpleMessage message)
		{
			DspRequest request = new DspRequest();
			request.DSP_MSG.Value = message;

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}
		/// <summary>
		/// Displays a message in the PinPad display with the unmanaged, unsafe, method
		/// The message is not garanteed to be displayed as expected, as each PinPad may have a different display size
		/// </summary>
		/// <param name="message">Message to display</param>
		/// <returns>true if message is displayed in the PinPad</returns>
		public bool DisplayMessage(MultilineMessage message) 
		{
			DexRequest request = new DexRequest( );
			request.DEX_MSG.Value = message;

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}

		// Interfaced methods
		/// <summary>
		/// Show message on pinpad screen.
		/// </summary>
		/// <param name="firstLine">The first line of the message, shown at the first screen line. Must have 16 characters or less.</param>
		/// <param name="secondLine">The second line of the message, shown at the second screen line. Must have 16 characters or less.</param>
		/// <param name="paddingType">At what alignment the message is present. It default value is left alignment.</param>
		/// <returns>Whether the message could be shown with success or not.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">This exception is thrown only if one (or both) of the messages exceed the limit of 16 characters.</exception>
		public bool ShowMessage(string firstLine, string secondLine = null, Sdk.Model.DisplayPaddingType paddingType = DisplayPaddingType.Left)
		{
			if (firstLine != null && firstLine.Length > 16) { firstLine = firstLine.Substring(0, 16); }
			if (secondLine != null && secondLine.Length > 16) { secondLine = secondLine.Substring(0, 16); }

			try
			{
				SimpleMessage message = new SimpleMessage(firstLine, secondLine, paddingType);

				return this.DisplayMessage (message);
			}
			catch (ArgumentOutOfRangeException) { throw; }
			catch (Exception) { return false; }
		}
	}
}
