using Pinpad.Core.Rebuild.Property;
using System.Collections.Generic;

namespace Pinpad.Core.Rebuild
{
	public interface ICommandRequest
	{
		Dictionary<PropertyCode, IProperty> Properties { get; }
		IContext CommandContext { get; }
		string Name { get; }
    }
}
