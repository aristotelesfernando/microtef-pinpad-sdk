namespace Pinpad.Core.Rebuild.Gertec
{
	/// <summary>
	/// Indicates the message to be presented on pinpad's first line.
	/// </summary>
	public enum GertecFirstLabelCode
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
		/// Presents "Digite".
		/// </summary>
		Type = 2,
		/// <summary>
		/// Presents "Redigite".
		/// </summary>
		Retype = 3,
		/// <summary>
		/// Presents "Entre".
		/// </summary>
		Enter = 4,
		/// <summary>
		/// Presents "Re-entre".
		/// </summary>
		ReEnter = 5,
		/// <summary>
		/// Presents "Insira".
		/// </summary>
		Insert = 6,
		/// <summary>
		/// Presents "Reinsira".
		/// </summary>
		Reinsert = 7,
		/// <summary>
		/// Presents "Numero".
		/// </summary>
		Number = 11,
		/// <summary>
		/// Presents "Digite o numero".
		/// </summary>
		TypeNumber = 12,
		/// <summary>
		/// Presents "Redigite numero".
		/// </summary>
		RetypeNumber = 13,
		/// <summary>
		/// Presents "Entre o numero".
		/// </summary>
		EnterNumber = 14,
		/// <summary>
		/// Presents "Re-entre numero".
		/// </summary>
		ReEnterNumber = 15,
		/// <summary>
		/// Presents "Insira o numero".
		/// </summary>
		InsertNumber = 16,
		/// <summary>
		/// Presents "Reinsira numero".
		/// </summary>
		ReinsertNumber = 17,
		/// <summary>
		/// Presents "Codigo".
		/// </summary>
		Code = 21,
		/// <summary>
		/// Presents "Digite o codigo".
		/// </summary>
		TypeCode = 22,
		/// <summary>
		/// Presents "Redigite codigo".
		/// </summary>
		RetypeCode = 23,
		/// <summary>
		/// Presents "Entre o codigo".
		/// </summary>
		EnterCode = 24,
		/// <summary>
		/// Presents "Re-entre codigo".
		/// </summary>
		ReEnterCode = 25,
		/// <summary>
		/// Presents "Insira o codigo".
		/// </summary>
		InsertCode = 26,
		/// <summary>
		/// Presents "Reinsira codigo".
		/// </summary>
		ReinsertCode = 27
	}

	/// <summary>
	/// Indicates the message to be presented on pinpad's second line.
	/// </summary>
	public enum GertecSecondLabelCode
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
