namespace Pinpad.Core.Rebuild
{
	public interface IContext
	{
		byte StartByte { get; }
		byte EndByte { get; }

		byte [] GetIntegrityCode (byte [] data);
		bool IsIntegrityCodeValid (byte [] firstCode, byte [] secondCode);
	}
}
