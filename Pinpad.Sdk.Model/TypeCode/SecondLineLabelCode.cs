namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Indicates the message to be presented on pinpad's second line.
	/// </summary>
	public enum SecondLineLabelCode
	{
		/// <summary>
		/// Undefined message. Sending this value to the SPE will throw an exception.
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// None text is presented.
		/// </summary>
		BlankSpace = 1,
		/// <summary>
		/// Presents "Numero telefone".
		/// </summary>
		TelephoneNumber = 2,
		/// <summary>
		/// Presents "Codigo de area".
		/// </summary>
		TelephoneAreaCode = 3,
		/// <summary>
		/// Presents "Cod. area + tel.".
		/// </summary>
		TelephoneNumberAndArea = 4,
		/// <summary>
		/// Presents "CPF".
		/// </summary>
		SocialSecurityDocument = 11,
		/// <summary>
		/// Presents "CNPJ".
		/// </summary>
		CnpjDocument = 12,
		/// <summary>
		/// Presents "RG".
		/// </summary>
		RgDocument = 13,
		/// <summary>
		/// Presents "PIS".
		/// </summary>
		PisDocument = 14,
		/// <summary>
		/// Presents "SUS".
		/// </summary>
		HealthDocument = 15,
		/// <summary>
		/// Presents "CNH".
		/// </summary>
		DriversLicense = 16,
		/// <summary>
		/// Presents "Passaporte".
		/// </summary>
		Passport = 17,
		/// <summary>
		/// Presents "Ult. 4 digitos".
		/// </summary>
		Last4Digits = 21,
		/// <summary>
		/// Presents "Cod. segurança".
		/// </summary>
		CardSecurityCode = 22,
		/// <summary>
		/// Presents "Bomba gasolina".
		/// </summary>
		GasPump = 81,
		/// <summary>
		/// Presents "Balança".
		/// </summary>
		Scale = 82,
		/// <summary>
		/// Presents "Ponto de Venda".
		/// </summary>
		PointOfSale = 91,
		/// <summary>
		/// Presents "Estação".
		/// </summary>
		Station = 92,
		/// <summary>
		/// Presents "Caixa".
		/// </summary>
		Cashier = 93,
		/// <summary>
		/// Presents "Terminal".
		/// </summary>
		Terminal = 94
	}
}
