using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Core.Rebuild.Property;
using Pinpad.Core.Rebuild.Gertec;

namespace Pinpad.Core.Rebuild.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1 ()
		{
			// Ops...
			//ICommandRequest command = new CommandBuilder(CommandCode.Open)
			//	.AddLabel("nao existe")
			//	.Build();

			//ICommandRequest command = new CommandBuilder(CommandCode.GetKeyboardInput)
			//	.Add(PropertyCode.FirstLineLabel, "Hello, World!")
			//	.Build();

			ICommandRequest command = new CommandBuilder(CommandCode.GetKeyboardInput)
				.Add(PropertyCode.FirstLineLabel, new FixedLengthProperty<GertecFirstLabelCode>(2, GertecFirstLabelCode.EnterNumber))
				.Add(PropertyCode.SecondLineLabel, new FixedLengthProperty<GertecSecondLabelCode>(2, GertecSecondLabelCode.DriversLicense))
				.Build();

		}
	}
}
