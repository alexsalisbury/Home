namespace Home.Core.DiscordBot.Tests.Clients
{
    using Xunit;
    using Home.Core.DiscordBot.Clients;

    public class ProductionDiscordClient_Tests
    {
        [Fact]
        public void BasicClientTest()
        {
            var pdc = new ProductionDiscordClient(null);
            Assert.NotNull(pdc);
        }
    }
}
