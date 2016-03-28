using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Core.Rebuild.Abecs
{
	public abstract class AbecsRequestBase : ICommandRequest
	{
		public IContext CommandContext { get; private set; }
		public abstract string Name { get; }

		public AbecsRequestBase ()
		{
			this.CommandContext = new AbecsContext();
		}

		public abstract void Add ();
		public void Send ()
		{
			throw new NotImplementedException();
		}
	}
}
