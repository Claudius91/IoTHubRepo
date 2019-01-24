using ApiServer;
using Server.Manager;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            IHub hub = new IoTHub(null);
            await Assert.ThrowsAsync<Exception>(() => hub.SendNotification(String.Empty));

        }
    }
}
