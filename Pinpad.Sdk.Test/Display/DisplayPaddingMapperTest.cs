using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Display.Mapper;
using PinPadSDK.Enums;
using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk.Test.Display
{
    [TestClass]
    public class DisplayPaddingMapperTest
    {
        [TestMethod]
        public void DisplayPaddingMapper_should_return_PaddingTypeLeft()
        {
            PaddingType padding = DisplayPaddingMapper.MapPaddingType(DisplayPaddingType.Left);
            Assert.AreEqual(padding, PaddingType.Left);
        }

        [TestMethod]
        public void DisplayPaddingMapper_should_return_PaddingTypeRight()
        {
            PaddingType padding = DisplayPaddingMapper.MapPaddingType(DisplayPaddingType.Right);
            Assert.AreEqual(padding, PaddingType.Right);
        }

        [TestMethod]
        public void DisplayPaddingMapper_should_return_PaddingTypeCenter()
        {
            PaddingType padding = DisplayPaddingMapper.MapPaddingType(DisplayPaddingType.Center);
            Assert.AreEqual(padding, PaddingType.Center);
        }

        [TestMethod]
        public void DisplayPaddingMapper_should_return_PaddingTypeUndefined()
        {
            PaddingType padding = DisplayPaddingMapper.MapPaddingType(DisplayPaddingType.Undefined);
            Assert.AreEqual(padding, PaddingType.Undefined);
        }
    }
}
