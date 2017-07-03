using NUnit.Framework;
using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test.Printer
{
    [TestFixture]
    public class PrinterItemTest
    {
        [Test]
        public void PrinterItem_Construction_ShouldNotReturnNull()
        {
            // Arrange && Act
            PrinterItemTest item = new PrinterItemTest();

            // Assert
            Assert.IsNotNull(item);
        }
        [Test]
        public void PrinterItem_Construction_ShouldReturnTypeDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.Type, default(IngenicoPrinterAction));
        }
        [Test]
        public void PrinterItem_Construction_ShouldReturnTextDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.Text, default(string));
        }
        [Test]
        public void PrinterItem_Construction_ShouldReturnAlignmentDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.Alignment, default(PrinterAlignmentCode));
        }
        [Test]
        public void PrinterItem_Construction_ShouldReturnFontSizeDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.FontSize, default(PrinterFontSize));
        }
        [Test]
        public void PrinterItem_Construction_ShouldReturnStopsToSkipDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.StepsToSkip, default(int));
        }
    }
}
