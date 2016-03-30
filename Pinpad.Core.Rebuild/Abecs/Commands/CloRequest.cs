using System;
using System.Collections.Generic;
using Pinpad.Core.Rebuild.Property;

namespace Pinpad.Core.Rebuild.Abecs
{
	internal class CloRequest : ICommandRequest
	{
		public IContext CommandContext { get; private set; }

		public string Name { get { return "CLO"; } }

		public Dictionary<PropertyCode, object> Properties { get; private set; }

		public CloRequest()
		{
			this.Properties[PropertyCode.Name] = this.Name;
			this.Properties[PropertyCode.Length] = 32;
			this.Properties[PropertyCode.FirstLineLabel] = "";
		}
	}
}
