using System;

namespace Pinpad.Sdk.Model.Exceptions
{
	/// <summary>
	/// Exception for reading a card as magnetic stripe while it has a EMV chip.
	/// </summary>
	public class CardHasChipException : Exception
	{
		/// <summary>
		/// Creates the exception with a predefined message.
		/// </summary>
		public CardHasChipException ()
			: base ("A card was read as magnetic stripe, but has chip. Please insert the EMV (chip & pin) card.") { }
	}
}
