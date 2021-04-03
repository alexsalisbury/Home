namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models;

    public class DiscordChannel_Tests
    {
        [Fact]
        public void BasicDiscordChannel()
        {
            var dc = new DiscordChannel(string.Empty, null);
            Assert.NotNull(dc);
        }
    }
}
