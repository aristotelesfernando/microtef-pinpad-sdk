using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Commands;

namespace Pinpad.Sdk.Test.Context
{
	[TestClass]
	public class GertecContextTest
	{
		public GertecContext Context;

		[TestInitialize]
		public void Setup ()
		{
			MicroPos.Platform.Desktop.DesktopInitializer.Initialize();
			this.Context = new GertecContext();
		}

		[TestMethod]
		public void GetIntegrityCode_should_return_2 ()
		{
			byte [] commandData = new byte [] { 02, 12, 49, 48, 49, 50, 56, 49, 48, 49, 48, 51, 54, 48, 03 };

			byte lrc = this.Context.GetIntegrityCode(commandData)[0];

			Assert.AreEqual(lrc, 2);
		}
	}
}
