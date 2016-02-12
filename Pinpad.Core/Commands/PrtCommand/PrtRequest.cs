using Pinpad.Core.Properties;
using Pinpad.Core.TypeCode;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Commands
{
	/// <summary>
	/// PRT request
	/// </summary>
	public class PrtRequest : BaseStoneRequest 
	{
		// Members
		/// <summary>
		/// Minimum stone version required for the request
		/// </summary>
		public override int MinimumStoneVersion { get { return 1; } }
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "PRT"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
		/// <summary>
		/// Action
		/// </summary>
		public virtual PrinterActionCode PRT_ACTION 
		{
			get 
			{
				if (this.PRT_ACTIONDATA.HasValue) 
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
		/// Action Data
		/// </summary>
		public SimpleProperty<BasePrtRequestData> PRT_ACTIONDATA { get; private set; }
		/// <summary>
		/// Begin action data
		/// </summary>
		public PrtBeginRequestData BeginData 
		{
			get 
			{
				return this.PRT_ACTIONDATA.Value as PrtBeginRequestData;
			}
			set 
			{
				this.PRT_ACTIONDATA.Value = value;
			}
		}
		/// <summary>
		/// End action data
		/// </summary>
		public PrtEndRequestData EndData 
		{
			get 
			{
				return this.PRT_ACTIONDATA.Value as PrtEndRequestData;
			}
			set 
			{
				this.PRT_ACTIONDATA.Value = value;
			}
		}
		/// <summary>
		/// Append string action data
		/// </summary>
		public PrtAppendStringRequestData AppendStringData 
		{
			get 
			{
				return this.PRT_ACTIONDATA.Value as PrtAppendStringRequestData;
			}
			set 
			{
				this.PRT_ACTIONDATA.Value = value;
			}
		}
		/// <summary>
		/// Append image action data
		/// </summary>
		public PrtAppendImageRequestData AppendImageData 
		{
			get 
			{
				return this.PRT_ACTIONDATA.Value as PrtAppendImageRequestData;
			}
			set 
			{
				this.PRT_ACTIONDATA.Value = value;
			}
		}
		/// <summary>
		/// Step action data
		/// </summary>
		public PrtStepRequestData StepData 
		{
			get 
			{
				return this.PRT_ACTIONDATA.Value as PrtStepRequestData;
			}
			set 
			{
				this.PRT_ACTIONDATA.Value = value;
			}
		}
		
		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.PRT_ACTIONDATA = new SimpleProperty<BasePrtRequestData>("PRT_ACTIONDATA", false, DefaultStringFormatter.PropertyControllerStringFormatter, PrtRequest.ActionDataStringParser);

			this.StartRegion(this.CMD_LEN1);
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
		/// <returns>PinPadPrtBaseRequestController</returns>
		private static BasePrtRequestData ActionDataStringParser(StringReader reader) 
		{
			string commandString = reader.ReadString(reader.Remaining);

			BasePrtRequestData value = new BasePrtRequestData();
			value.CommandString = commandString;
			switch (value.PRT_ACTION) 
			{
				case PrinterActionCode.Begin:
					value = new PrtBeginRequestData();
					break;

				case PrinterActionCode.End:
					value = new PrtEndRequestData();
					break;

				case PrinterActionCode.AppendString:
					value = new PrtAppendStringRequestData();
					break;

				case PrinterActionCode.AppendImage:
					value = new PrtAppendImageRequestData();
					break;

				case PrinterActionCode.Step:
					value = new PrtStepRequestData();
					break;

				default:
					throw new InvalidOperationException("Attempt to parse unknown action: " + value.PRT_ACTION);
			}
			value.CommandString = commandString;
			return value;
		}
	}
}
