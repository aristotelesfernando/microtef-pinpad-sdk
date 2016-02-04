using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Controllers.Tables;
using PinPadSdkTests.Tables;
using PinPadSDK.Exceptions;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class TlrRequestTests {
        [TestMethod]
        public void ValidateTlrRequestWarnings() {
            TlrRequest request = new TlrRequest();

            Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(request, "TLR_REC"), "Did not complain about lack of table Records");

            EmvAidTable aidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(aidTable);
            request.TLR_REC.Value.Add(aidTable);
            Assert.AreEqual("TLR31601" + aidTable.CommandString,
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            request.TLR_REC.Value.Clear();
            request.TLR_REC.Value.Add(capkTable);
            Assert.AreEqual("TLR61301" + capkTable.CommandString,
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            request.TLR_REC.Value.Clear();
            request.TLR_REC.Value.Add(revCerTable);
            Assert.AreEqual("TLR02801" + revCerTable.CommandString,
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);

            request.TLR_REC.Value.Clear();
            request.TLR_REC.Value.Add(aidTable);
            request.TLR_REC.Value.Add(capkTable);
            request.TLR_REC.Value.Add(revCerTable);
            Assert.AreEqual("TLR95303" + aidTable.CommandString + capkTable.CommandString + revCerTable.CommandString,
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }

        [TestMethod]
        public void ValidateUnknownAidTableTlrRequestWarnings() {
            TlrRequest request = new TlrRequest();

            Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(request, "TLR_REC"), "Did not complain about lack of table Records");

            UnknownAidTable aidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(aidTable);
            request.TLR_REC.Value.Add(aidTable);
            Assert.AreEqual("TLR16401" + aidTable.CommandString,
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }

        [TestMethod]
        public void ValidateUnknownTableTlrRequestWarnings() {
            TlrRequest request = new TlrRequest();

            Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(request, "TLR_REC"), "Did not complain about lack of table Records");

            try {
                request.CommandString = "TLR0280102641212123456789012123456";
                Assert.Fail("Did not complain about unknown Table type");
            }
            catch (PropertyParseException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException), "Did not complain about unknown Table type");
            }
        }
    }
}
