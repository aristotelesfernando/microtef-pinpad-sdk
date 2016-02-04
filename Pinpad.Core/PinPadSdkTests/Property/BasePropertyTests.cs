using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Property;

namespace PinPadSdkTests.Property {
    [TestClass]
    public class BasePropertyTests {
        public class BasePropertyMock : BaseProperty {
            public new void EndLastRegion() {
                base.EndLastRegion();
            }
        }
        [TestMethod]
        public void ValidateInvalidEndLastRegionError() {
            BasePropertyMock property = new BasePropertyMock();

            try {
                property.EndLastRegion();
                Assert.Fail("Did not complain about ending last region without any active regions");
            }
            catch (InvalidOperationException) {
            }
        }
    }
}
