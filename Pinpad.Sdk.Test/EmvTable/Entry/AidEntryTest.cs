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
            AidEntry aid = new AidEntry();
            Assert.IsNotNull(aid);
        }

        [TestMethod]
        public void AidEntry_should_extend_BaseTableEntry()
        {
            AidEntry aid = new AidEntry();
            Type aidBaseType = aid.GetType().BaseType;
            Assert.AreEqual(aidBaseType, typeof(BaseTableEntry));
        }
    }
}
