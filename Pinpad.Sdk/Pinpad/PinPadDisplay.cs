using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Model;
using System;

namespace Pinpad.Sdk 
{
	/// <summary>
	/// Pinpad display API.
	/// </summary>
	public class PinpadDisplay : IPinpadDisplay
	{
		// Constants
		public const int DISPLAY_LINE_WIDTH = 16;

		// Members
		/// <summary>
		/// Pinpad communication.
		/// </summary>
		private PinpadCommunication communication;

		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinPad">PinPad to use</param>
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
