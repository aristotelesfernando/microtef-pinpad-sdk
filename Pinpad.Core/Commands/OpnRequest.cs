namespace Pinpad.Core.Commands 
{
	/// <summary>
	/// OPN request
	/// </summary>
	public class OpnRequest : BaseCommand 
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
