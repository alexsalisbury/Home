namespace Home.Core.DiscordBot.Tests.Clients
{
    using System.Threading.Tasks;
    using Xunit;
    using Home.Core.Models.Settings;
    using Home.Core.Tests.Mocks;
    using Home.Core.DiscordBot.Clients;

    public class HomeClient_Tests
    {
        [Fact]
        public void BasicClientTest()
        {
            var scc = new ShyCloudClient(null);
            Assert.NotNull(scc);
        }
    }
}
