using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Pinpad_commands
{
	internal interface IProperty
	{
		string Value { get; }
		int Length { get; }
	}
}
