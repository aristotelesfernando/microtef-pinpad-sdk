using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Pinpad;
using NUnit.Framework;

namespace Pinpad.Sdk.Test.Printer
{
    [TestFixture]
    public class IngenicoPinpadPrinterTest
    {
        public IngenicoPinpadPrinter Printer { get; set; }

        [SetUp]
        public void Setup ()
        {
            this.Printer = new IngenicoPinpadPrinter(
                new Stubs.PinpadCommunicationStub(),
                new Stubs.PinpadInfosStub());
        }

        [Test]
        public void IngenicoPinpadPrinter_Itself_ShouldInheritIPinpadPrinter()
        {
            // Assert
            Assert.IsInstanceOf(typeof(IPinpadPrinter), this.Printer);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddQrCode_ShouldNotReturnNull_IfMethodIsCorrect()
        {
            // Arrange
            string qrCodeMessage = "I'm a QR code!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;

            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AddQrCode(alignment,
                qrCodeMessage);

            // Assert
            Assert.IsNotNull(returnedPrinter);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddQrCode_ShouldReturnItself_IfMethodIsCorrect ()
        {
            // Arrange
            string qrCodeMessage = "I'm a QR code!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;

            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AddQrCode(alignment,
                qrCodeMessage);

            // Assert
            Assert.AreEqual(returnedPrinter, this.Printer);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddQrCode_ShouldAddOneQrCodeItemToPrinterBuffer_IfMethodIsCorrect()
        {
            // Arrange
            string qrCodeMessage = "I'm a QR code!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;
            int expectedItemsToPrint = 1;

            // Act
            this.Printer.AddQrCode(alignment, qrCodeMessage);

            // Assert
            Assert.AreEqual(expectedItemsToPrint, this.Printer.ItemsToPrint.Count);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddQrCode_ShouldMatchQrCodeMessage_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            string expectedQrCodeMessage = "I'm a QR code!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;

            // Act
            this.Printer.AddQrCode(alignment, expectedQrCodeMessage);

            // Assert
            Assert.AreEqual(expectedQrCodeMessage, this.Printer.ItemsToPrint[0].Text);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddQrCode_ShouldMatchQrCodeAlignment_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            string qrCodeMessage = "I'm a QR code!";
            PrinterAlignmentCode expectedAlignment = PrinterAlignmentCode.Center;

            // Act
            this.Printer.AddQrCode(expectedAlignment, qrCodeMessage);

            // Assert
            Assert.AreEqual(expectedAlignment, this.Printer.ItemsToPrint[0].Alignment);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddQrCode_ShouldMatchPrintingType_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            string qrCodeMessage = "I'm a QR code!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;
            IngenicoPrinterAction expectedType = IngenicoPrinterAction.PrintQrCode;

            // Act
            this.Printer.AddQrCode(alignment, qrCodeMessage);

            // Assert
            Assert.AreEqual(expectedType, this.Printer.ItemsToPrint[0].Type);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddQrCode_ShouldMatchQrCodeSize_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            string qrCodeMessage = "I'm a QR code!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;
            PrinterFontSize expectedQrCodeSize = PrinterFontSize.Big;

            // Act
            this.Printer.AddQrCode(alignment, qrCodeMessage);

            // Assert
            Assert.AreEqual(expectedQrCodeSize, this.Printer.ItemsToPrint[0].FontSize);
        }

        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouNotReturnNull_IfParametersArePassed()
        {
            // Arrange
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Left;
            PrinterFontSize size = PrinterFontSize.Medium;
            string messageToPrint = "I'm a happy text!";

            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AppendLine(alignment, size, 
                messageToPrint);

            // Assert
            Assert.IsNotNull(returnedPrinter);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouNotReturnNull_IfNoParametersArePassed()
        {
            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AppendLine();

            // Assert
            Assert.IsNotNull(returnedPrinter);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldReturnItself_IfMethodIsCorrectAndParametersArePassed()
        {
            // Arrange
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Left;
            PrinterFontSize size = PrinterFontSize.Medium;
            string messageToPrint = "I'm a happy text!";

            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AppendLine(alignment, size,
                messageToPrint);

            // Assert
            Assert.AreEqual(returnedPrinter, this.Printer);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldReturnItself_IfMethodIsCorrectAndNoParametersArePassed()
        {
            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AppendLine();

            // Assert
            Assert.AreEqual(returnedPrinter, this.Printer);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldOneTextItemToPrinterBuffer_IfParametersArePassed()
        {
            // Arrange
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Left;
            PrinterFontSize size = PrinterFontSize.Medium;
            string messageToPrint = "I'm a happy text!";
            int expectedItemsToPrint = 1;

            // Act
            this.Printer.AppendLine(alignment, size, messageToPrint);

            // Assert
            Assert.AreEqual(expectedItemsToPrint, this.Printer.ItemsToPrint.Count);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldMatchPrintingType_IfParametersArePassedAndMethodIsMappingItCorrectly()
        {
            // Arrange
            string text = "I'm a happy text!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;
            PrinterFontSize size = PrinterFontSize.Medium;
            IngenicoPrinterAction expectedType = IngenicoPrinterAction.PrintText;

            // Act
            this.Printer.AppendLine(alignment, size, text);

            // Assert
            Assert.AreEqual(expectedType, this.Printer.ItemsToPrint[0].Type);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldMatchTextToPrint_IfParametersArePassedAndMethodIsMappingItCorrectly()
        {
            // Arrange
            string expectedText = "I'm a happy text!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;
            PrinterFontSize size = PrinterFontSize.Medium;

            // Act
            this.Printer.AppendLine(alignment, size, expectedText);

            // Assert
            Assert.AreEqual(expectedText, this.Printer.ItemsToPrint[0].Text);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldMatchAlignment_IfParametersArePassedAndMethodIsMappingItCorrectly()
        {
            // Arrange
            string text = "I'm a happy text!";
            PrinterAlignmentCode expectedAlignment = PrinterAlignmentCode.Center;
            PrinterFontSize size = PrinterFontSize.Medium;

            // Act
            this.Printer.AppendLine(expectedAlignment, size, text);

            // Assert
            Assert.AreEqual(expectedAlignment, this.Printer.ItemsToPrint[0].Alignment);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldMatchFontSize_IfParametersArePassedAndMethodIsMappingItCorrectly()
        {
            // Arrange
            string text = "I'm a happy text!";
            PrinterAlignmentCode alignment = PrinterAlignmentCode.Center;
            PrinterFontSize expectedSize = PrinterFontSize.Medium;

            // Act
            this.Printer.AppendLine(alignment, expectedSize, text);

            // Assert
            Assert.AreEqual(expectedSize, this.Printer.ItemsToPrint[0].FontSize);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldMatchPrintingType_IfNoParametersArePassedAndMethodIsMappingItCorrectly()
        {
            // Arrange
            IngenicoPrinterAction expectedType = IngenicoPrinterAction.SkipLine;

            // Act
            this.Printer.AppendLine();

            // Assert
            Assert.AreEqual(expectedType, this.Printer.ItemsToPrint[0].Type);
        }
        [Test]
        public void IngenicoPinpadPrinter_AppendLine_ShouldMatchStepsToSkip_IfNoParametersArePassedAndMethodIsMappingItCorrectly()
        {
            // Arrange
            int expectedStepsToSkip = 4;

            // Act
            this.Printer.AppendLine();

            // Assert
            Assert.AreEqual(expectedStepsToSkip, this.Printer.ItemsToPrint[0].StepsToSkip);
        }

        [Test]
        public void IngenicoPinpadPrinter_AddSeparator_ShouNotReturnNull_IfMethodIsCorrect()
        {
            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AddSeparator();

            // Assert
            Assert.IsNotNull(returnedPrinter);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddSeparator_ShouldReturnItself_IfMethodIsCorrect()
        {
            // Act
            IPinpadPrinter returnedPrinter = this.Printer.AddSeparator();

            // Assert
            Assert.AreEqual(returnedPrinter, this.Printer);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddSeparator_ShouldMatchPrintingType_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            IngenicoPrinterAction expectedPrintingType = IngenicoPrinterAction.PrintText;

            // Act
            this.Printer.AddSeparator();

            // Assert
            Assert.AreEqual(expectedPrintingType, this.Printer.ItemsToPrint[0].Type);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddSeparator_ShouldMatchAlignment_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            PrinterAlignmentCode expectedAlignment = PrinterAlignmentCode.Center;

            // Act
            this.Printer.AddSeparator();

            // Assert
            Assert.AreEqual(expectedAlignment, this.Printer.ItemsToPrint[0].Alignment);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddSeparator_ShouldMatchFontSize_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            PrinterFontSize expectedFontSize = PrinterFontSize.Big;

            // Act
            this.Printer.AddSeparator();

            // Assert
            Assert.AreEqual(expectedFontSize, this.Printer.ItemsToPrint[0].FontSize);
        }
        [Test]
        public void IngenicoPinpadPrinter_AddSeparator_ShouldMatchText_IfMethodIsMappingItCorrectly()
        {
            // Arrange
            string expectedText = "________________________";

            // Act
            this.Printer.AddSeparator();

            // Assert
            Assert.AreEqual(expectedText, this.Printer.ItemsToPrint[0].Text);
        }
    }
}
