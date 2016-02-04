using CrossPlatformBase;
using CrossPlatformDesktop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPadSdkTests {
    public class SendMailControllerMock : ISendMailController {
        public void SendReportMail(string subject, string body) {
            Debug.WriteLine(subject + Environment.NewLine + body, "Report Email");
        }

        public void SendReportMailThreaded(string subject, string body) {
            this.SendReportMail(subject, body);
        }

        public ISendMail CreateNewMail() {
            throw new NotImplementedException();
        }
    }
}
