using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test.EmvTable
{
    [TestClass]
    public class CapkEntryTest
    {
        [TestMethod]
        public void CapkEntry_should_not_return_null()
        {
            CapkEntry capk = new CapkEntry();
            Assert.IsNotNull(capk);
        }

        [TestMethod]
        public void CapkEntry_should_extend_BaseTableEntry()
        {
            CapkEntry capk = new CapkEntry();
            Type capkBaseType = capk.GetType().BaseType;
            Assert.AreEqual(capkBaseType, typeof(BaseTableEntry));
        }
    }
}
