using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class CardEntryTest
    {
        [TestMethod]
        public void CardEntry_should_not_return_null()
        {
            CardEntry card = new CardEntry();
            Assert.IsNotNull(card);
        }
    }
}
