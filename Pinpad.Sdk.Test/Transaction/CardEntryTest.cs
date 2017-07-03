using Pinpad.Sdk.Model;
using NUnit.Framework;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestFixture]
    public class CardEntryTest
    {
        [Test]
        public void CardEntry_Construction_ShouldNotReturnNull ()
        {
            // Arrange and act
            CardEntry card = new CardEntry();

            // Assert
            Assert.IsNotNull(card);
        }
    }
}
