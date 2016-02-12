using Pinpad.Core.Commands;
using Pinpad.Core.Properties;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Pinpad 
{
	/// <summary>
	/// PinPad display tool
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
		/// <summary>
		/// If images are supported.
		/// </summary>
		private Lazy<bool> imagesSupported { get; set; }

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
			this.imagesSupported = new Lazy<bool>(this.AreImagesSupported);
		}

		// Methods
		private bool AreImagesSupported () 
		{
			return this.communication.StoneVersion >= new DsiRequest().MinimumStoneVersion;
		}
		/// <summary>
		/// Displays a message in the PinPad display with the default, safe, method
		/// </summary>
		/// <param name="message">Message to display</param>
		/// <returns>true if message is displayed in the PinPad</returns>
		public bool DisplayMessage(SimpleMessage message) {
			DspRequest request = new DspRequest( );
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
		/// <summary>
		/// Display a previously loaded image
		/// </summary>
		/// <param name="imageName">Image name, up to 15 characters</param>
		/// <returns>true if the message was displayed</returns>
		public bool DisplayImage(string imageName) 
		{
			DsiRequest request = new DsiRequest();
			request.DSI_IMGNAME.Value = imageName;

			return this.communication.SendRequestAndVerifyResponseCode(request);
		}

		// Interfaced methods
		public bool ShowMessage(string firstLine, string secondLine = null, Sdk.Model.TypeCode.DisplayPaddingType paddingType = DisplayPaddingType.Left)
		{
			if (firstLine != null && firstLine.Length > 16) { firstLine = firstLine.Substring(0, 16); }
			if (secondLine != null && secondLine.Length > 16) { secondLine = secondLine.Substring(0, 16); }

			try
			{

				SimpleMessage message = new SimpleMessage(firstLine, secondLine, paddingType);

				this.DisplayMessage(message);

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
