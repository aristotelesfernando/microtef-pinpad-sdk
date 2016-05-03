namespace Pinpad.Sdk.Model
{
	public interface IPinpadTransaction
	{
		/// <summary>
		/// On Pinpad screen, alternates between "RETIRE O CARTÃO" and parameter 'message' received, until card removal.
		/// </summary>
		/// <param name="message">Message to be shown on pinpad screen. Must not exceed 16 characters. This message remains on Pinpad screen after card removal.</param>
		/// <param name="padding">Message alignment.</param>
		/// <returns></returns>
		bool RemoveCard (string message, DisplayPaddingType padding);
		/// <summary>
		/// Read basic card information, that is, brand id, card type, card primary account number (PAN), cardholder name and expiration date.
		/// If the card is removed in the middle of the process, returns CANCEL status.
		/// </summary>
		/// <param name="transactionType">Transaction type, that is, debit/credit.</param>
		/// <returns>Card basic info.</returns>
		/// <exception cref="ExpiredCardException">When an expired card is read.</exception>
		CardEntry ReadCard (TransactionType transactionType, decimal amount, out TransactionType newTransactionType);
		/// <summary>
		/// If cardholder card needs password, than prompts it. Otherwise, nothing is done. 
		/// </summary>
		/// <param name="amount">Transaction amount in cents.</param>
		/// <param name="pan">Primary Account Number (PAN).</param>
		/// <param name="readingMode">Card type, defining how the card must be read.</param>
		/// <returns>An object Pin, containing pin block (cardholder password), Key Serial Number (KSN, determining pin block DUKPT encryption key) and if pin verification is online. </returns>
		Pin ReadPassword (decimal amount, string pan = "", CardType readingMode = CardType.Emv);
	}
}
