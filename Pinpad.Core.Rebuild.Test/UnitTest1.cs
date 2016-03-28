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
			IPinpadResponse resp1 = new GertecResponse();
			Assert.AreEqual(resp1.ResponseCode, GertecResponseCode.Xpto2);

			if ((GertecResponseCode) resp1.ResponseCode == GertecResponseCode.Xpto2)
			{

			}

			resp1 = new AbecsResponse();
			Assert.AreEqual(resp1.ResponseCode, AbecsResponseCode.Abcd1);
		}
	}
}
