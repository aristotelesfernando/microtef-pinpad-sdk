namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Indicates the message to be presented on pinpad's first line.
	/// </summary>
	public enum FirstLineLabelCode
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
}
