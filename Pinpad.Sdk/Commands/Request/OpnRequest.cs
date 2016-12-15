namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// OPN request
	/// </summary>
	internal sealed class OpnRequest : BaseCommand 
	{
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "OPN"; } }
		
		/// <summary>
		/// Constructor
		/// </summary>
		public OpnRequest() {  }
	}
}
