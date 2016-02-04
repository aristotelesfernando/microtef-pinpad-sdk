using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Factories;
using PinPadSDK.Enums;
using PinPadSDK.Exceptions;

namespace PinPadSdkTests.Factories {
    [TestClass]
    public class PrinterExceptionFactoryTests {
        [TestMethod]
        public void ValidatePrinterExceptionFactoryUndefined() {
            try {
                PrinterExceptionFactory.Create(PinPadPrinterStatus.Undefined);
                Assert.Fail("Did not complain about Undefined Printer Status");
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryReady() {
            Assert.IsNull(PrinterExceptionFactory.Create(PinPadPrinterStatus.Ready));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryBusy() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.Busy), typeof(PrinterBusyException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryOutOfPaper() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.OutOfPaper), typeof(PrinterOutOfPaperException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryInvalidFormat() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.InvalidFormat), typeof(PrinterException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryPrinterError() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.PrinterError), typeof(PrinterException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryOverheating() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.Overheating), typeof(PrinterOverheatingException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryUnfinishedPrinting() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.UnfinishedPrinting), typeof(PrinterException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryLackOfFont() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.LackOfFont), typeof(PrinterException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryPackageTooLong() {
            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(PinPadPrinterStatus.PackageTooLong), typeof(PrinterException));
        }
        [TestMethod]
        public void ValidatePrinterExceptionFactoryDefault() {
            PinPadPrinterStatus unknownStatus = PinPadPrinterStatus.Undefined;

            for(int i=0; i<Int32.MaxValue; i++){
                if(Enum.IsDefined(typeof(PinPadPrinterStatus), i) == false){
                    unknownStatus = (PinPadPrinterStatus)i; //Find a value that is not in the enum
                    break;
                }
            }

            Assert.IsInstanceOfType(PrinterExceptionFactory.Create(unknownStatus), typeof(PrinterException));
        }
    }
}
