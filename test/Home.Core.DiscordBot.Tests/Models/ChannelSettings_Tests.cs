namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models.Settings;

    public class ChannelSettings_Tests
    {
        [Fact]
        public void BasicChannelSettings()
        {
            var cs = new ChannelSettings(string.Empty, null);
            Assert.NotNull(cs);
        }
    }
}
