using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test.EmvTable.Entry
{
    [TestClass]
    public class AidEntryTest
    {
        [TestMethod]
        public void AidEntry_should_not_return_null()
        {
            PinpadAid aid = new PinpadAid();
            Assert.IsNotNull(aid);
        }

        [TestMethod]
        public void AidEntry_should_extend_BaseTableEntry()
        {
            PinpadAid aid = new PinpadAid();
            Type aidBaseType = aid.GetType().BaseType;
            Assert.AreEqual(aidBaseType, typeof(BaseTableEntry));
        }
    }
}
