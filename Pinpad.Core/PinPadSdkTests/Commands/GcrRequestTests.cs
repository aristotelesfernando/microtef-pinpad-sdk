using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using System.Globalization;
using PinPadSDK.Commands.Gcr;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GcrRequestTests {
        [TestMethod]
        public void ValidateGcrRequestWarnings() {
            string timestring = "150102030405";
            DateTime dateTime = DateTime.ParseExact(timestring, "yyMMddHHmmss", CultureInfo.InvariantCulture);

            GcrRequest request = new GcrRequest();

            BasePropertyTestUtils.TestProperty(request, request.GCR_ACQIDXREQ, 2);
            BasePropertyTestUtils.TestProperty(request, request.GCR_APPTYPREQ, 2);
            BasePropertyTestUtils.TestProperty(request, request.GCR_AMOUNT, 12);
            BasePropertyTestUtils.TestProperty(request, request.GCR_DATE_TIME, dateTime);
            BasePropertyTestUtils.TestFixedProperty(request, request.GCR_TABVER, 10);

            Assert.AreEqual("GCR0401212123456789012" + timestring + "123456789000", request.CommandString);

            GcrIdApp idApp1 = new GcrIdApp(16, 17);
            request.GCR_IDAPP_Collection.Value.Add(idApp1);
            Assert.AreEqual("GCR0441212123456789012" + timestring + "123456789001" + idApp1.TAB_ACQ.Value.Value.ToString("d2") + idApp1.TAB_RECIDX.Value.Value.ToString("d2"), 
                request.CommandString);

            GcrIdApp idApp2 = new GcrIdApp(18, 19);
            request.GCR_IDAPP_Collection.Value.Add(idApp2);
            Assert.AreEqual("GCR0481212123456789012" + timestring + "123456789002" + idApp1.TAB_ACQ.Value.Value.ToString("d2") + idApp1.TAB_RECIDX.Value.Value.ToString("d2") + idApp2.TAB_ACQ.Value.Value.ToString("d2") + idApp2.TAB_RECIDX.Value.Value.ToString("d2"),
                request.CommandString);

            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
