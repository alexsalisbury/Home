namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models.Settings;

    public class BotSettings_Tests
    {
        [Fact]
        public void BasicBotSettings()
        {
            var bs = new BotSettings();
            Assert.NotNull(bs);
        }
    }
}
