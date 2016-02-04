using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pinpad.Sdk.Test.EmvTable.Entry
{
    [TestClass]
    public class BaseTableEntryTest
    {
        [TestMethod]
        public void BaseTableEntry_should_not_return_null()
        {
            BaseTableEntryTest baseTable = new BaseTableEntryTest();
            Assert.IsNotNull(baseTable);
        }
    }
}
