using Pinpad.Sdk.Commands;
using NUnit.Framework;

namespace Pinpad.Sdk.Test.Context
{
    public class GertecContextTest
    {
		internal GertecContext Context;

        [SetUp]
		public void Setup()
		{
			this.Context = new GertecContext();
		}

		[Test]
		public void GetIntegrityCode_should_return_2()
		{
			byte[] commandData = new byte[] { 02, 12, 49, 48, 49, 50, 56, 49, 48, 49, 48, 51, 54, 48, 03 };

			byte lrc = this.Context.GetIntegrityCode(commandData)[0];

			Assert.AreEqual(lrc, 2);
		}
    }
}
