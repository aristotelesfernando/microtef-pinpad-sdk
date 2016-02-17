using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Connection;
using System;
using System.Threading;
using Pinpad.Core.Commands;
using System.Diagnostics;

namespace Pinpad.Sdk.Test.Connection
{
	[TestClass]
	public class PinpadConnectionTest
	{
		[TestMethod]
		public void MyTestMethod()
		{
			GcdRequest r = new GcdRequest();
			r.SPE_MSGIDX.Value = Model.TypeCode.KeyboardMessageCode.AskForSecurityCode;
			r.SPE_MINDIG.Value = 1;
			r.SPE_MAXDIG.Value = 2;

			Debug.WriteLine((int)r.SPE_MSGIDX.Value);
			Debug.WriteLine(r.CommandString);
		}
	}
}
