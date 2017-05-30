using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Test.Mockings;

namespace Pinpad.Sdk.Test.UpdateService
{
    [TestClass]
    public sealed class UpdateServiceTest
    {
        [TestInitialize]
        public void Setup ()
        {
            ApplicationFileMocker.MockNewApplicationFile("StonePinpadWifi.1.2.3.zip");
        }
        [TestCleanup]
        public void Cleanup ()
        {
            ApplicationFileMocker.Unmock();
        }

        // TODO: Creation should throw exception if pinpad information is null
        [TestMethod]
        public void MyTestMethod()
        {
            ;
        }
        // TODO: Creation should throw exception if pinpad communication is null
        // TODO: Load should return true if the zipped application file exists
        // TODO: Load should return false if the zipped application file does not exist
        // TODO: Update should return false if the pinpad is not from Stone
        // TODO: Update should trhrow exception if the load was not previously called
        // TODO: Update should trhrow exception if the load was unsucessful
        // TODO: CurrentApplicationVersion should return the same version of the application file
    }
}
