using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test.Printer
{
    [TestClass]
    public class PrinterItemTest
    {
        [TestMethod]
        public void PrinterItem_Construction_ShouldNotReturnNull()
        {
            // Arrange && Act
            PrinterItemTest item = new PrinterItemTest();

            // Assert
            Assert.IsNotNull(item);
        }
        [TestMethod]
        public void PrinterItem_Construction_ShouldReturnTypeDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.Type, default(IngenicoPrinterAction));
        }
        [TestMethod]
        public void PrinterItem_Construction_ShouldReturnTextDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.Text, default(string));
        }
        [TestMethod]
        public void PrinterItem_Construction_ShouldReturnAlignmentDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.Alignment, default(PrinterAlignmentCode));
        }
        [TestMethod]
        public void PrinterItem_Construction_ShouldReturnFontSizeDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.FontSize, default(PrinterFontSize));
        }
        [TestMethod]
        public void PrinterItem_Construction_ShouldReturnStopsToSkipDefaultValue_IfConstructorDoesNotModifyIt()
        {
            // Arrange && Act
            PrinterItem item = new PrinterItem();

            // Assert
            Assert.AreEqual(item.StepsToSkip, default(int));
        }
    }
}
