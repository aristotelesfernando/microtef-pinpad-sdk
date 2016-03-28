namespace Pinpad.Core.Rebuild
{
	public enum CommandCode
	{
		Undefined = 0,

		Open,
		Close,

		ShowDisplayMessage,

		GetInformation,

		GetKeyboardInput,
		GetKeyboardControlKey,

		GoOnChip,
		GetPan,
		GetCard,
		RemoveCard,

		GetTableVersion,
		TableLoadInitialization,
		TableLoadRecord,
		TableLoadEnd
	}
}
