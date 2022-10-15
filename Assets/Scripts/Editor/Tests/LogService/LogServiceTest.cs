using NUnit.Framework;
using Services.Log;

namespace Editor.Tests
{
    public class LogServiceTest : IntegrationTestBase
    {
        [Test]
        [TestCase("WindowService", Services.Log.LogType.Message, "Hello Message!")]
        [TestCase("ProjectService", Services.Log.LogType.Warning, "Hello Warning!")]
        [TestCase("AbilityService", Services.Log.LogType.Error, "Hello Error!")]
        [TestCase("LogService", Services.Log.LogType.Message, "")]
        [TestCase("", Services.Log.LogType.Error, "")]
        public void _00_ShowBase(string name, Services.Log.LogType logType, string message)
        {
           var logService =  Container.Resolve<LogService>();

            logService.ShowLog(name, logType, message);

            Assert.IsTrue(true);
        }
    }
}
