using System;
using System.Collections.Generic;
using Pinpad.Core.Rebuild.Property;

namespace Pinpad.Core.Rebuild.Abecs
{
	internal class OpnRequest : ICommandRequest
	{
		public IContext CommandContext { get; private set; }

		public string Name { get { return "OPN"; } }

		public Dictionary<PropertyCode, object> Properties { get; private set; }

		public OpnRequest ()
		{
			this.CommandContext = new AbecsContext();
			this.Properties.Add(PropertyCode.Name, this.Name);
		}
	}
}
