using Pinpad.Sdk.Properties;
using Pinpad.Sdk.TypeCode;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// PRT response
	/// </summary>
	public class PrtResponse : BaseResponse 
	{
		// Members
		/// <summary>
		/// Is this a blocking command?
		/// </summary>
		public override bool IsBlockingCommand { get { return true; } }
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "PRT"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty RSP_LEN1 { get; private set; }
		/// <summary>
		/// Action
		/// </summary>
		public virtual PrinterActionCode PRT_ACTION 
		{
			get
			{
				if (this.PRT_ACTIONDATA.HasValue == true)
				{
					return this.PRT_ACTIONDATA.Value.PRT_ACTION;
				}
				else
				{
					return PrinterActionCode.Undefined;
				}
			}
		}
		/// <summary>
		/// Action data
		/// </summary>
		public SimpleProperty<BasePrtResponseData> PRT_ACTIONDATA { get; private set; }
		/// <summary>
		/// Begin action data
		/// </summary>
		public PrtBeginResponseData BeginData 
		{
			get { return this.PRT_ACTIONDATA.Value as PrtBeginResponseData; }
			set { this.PRT_ACTIONDATA.Value = value; }
		}
		/// <summary>
		/// End action data
		/// </summary>
		public PrtEndResponseData EndData 
		{
			get { return this.PRT_ACTIONDATA.Value as PrtEndResponseData; }
			set { this.PRT_ACTIONDATA.Value = value; }
		}
		/// <summary>
		/// Append string action data
		/// </summary>
		public PrtAppendStringResponseData AppendStringData 
		{
			get { return this.PRT_ACTIONDATA.Value as PrtAppendStringResponseData; }
			set { this.PRT_ACTIONDATA.Value = value; }
		}
		/// <summary>
		/// Append image action data
		/// </summary>
		public PrtAppendImageResponseData AppendImageData 
		{
			get { return this.PRT_ACTIONDATA.Value as PrtAppendImageResponseData; }
			set { this.PRT_ACTIONDATA.Value = value; }
		}
		/// <summary>
		/// Step action data
		/// </summary>
		public PrtStepResponseData StepData 
		{
			get { return this.PRT_ACTIONDATA.Value as PrtStepResponseData; }
			set { this.PRT_ACTIONDATA.Value = value; }
		}

		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
			this.PRT_ACTIONDATA = new SimpleProperty<BasePrtResponseData>("PRT_ACTIONDATA", false, DefaultStringFormatter.PropertyControllerStringFormatter, PrtResponse.ActionDataStringParser);

			this.StartRegion(this.RSP_LEN1);
			{
				this.AddProperty(this.PRT_ACTIONDATA);
			}
			this.EndLastRegion();
		}

		// Method
		/// <summary>
		/// Parses a Prt Action Data
		/// </summary>
		/// <param name="reader">string reader</param>
		/// <returns>PinPadPrtBaseResponseController</returns>
		private static BasePrtResponseData ActionDataStringParser(StringReader reader) 
		{
			string commandString = reader.ReadString(reader.Remaining);

			BasePrtResponseData value = new BasePrtResponseData();
			value.CommandString = commandString;
			switch (value.PRT_ACTION)
			{
				case PrinterActionCode.Begin:
					value = new PrtBeginResponseData();
					break;

				case PrinterActionCode.End:
					value = new PrtEndResponseData();
					break;

				case PrinterActionCode.AppendString:
					value = new PrtAppendStringResponseData();
					break;

				case PrinterActionCode.AppendImage:
					value = new PrtAppendImageResponseData();
					break;

				case PrinterActionCode.Step:
					value = new PrtStepResponseData();
					break;

				default:
					throw new InvalidOperationException("Attempt to parse unknown action: " + value.PRT_ACTION);
			}
			value.CommandString = commandString;
			return value;
		}
	}
}
