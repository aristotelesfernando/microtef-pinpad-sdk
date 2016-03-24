using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pinpad.Core.Rebuild.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1 ()
		{
			IPinpadResponse resp1 = new XptoResponse();
			Assert.AreEqual(resp1.ResponseCode, XptoResponseCode.Xpto2);

			if ((XptoResponseCode) resp1.ResponseCode == XptoResponseCode.Xpto2)
			{

			}

			resp1 = new AbcdResponse();
			Assert.AreEqual(resp1.ResponseCode, AbcdResponseCode.Abcd1);
		}
	}
}
