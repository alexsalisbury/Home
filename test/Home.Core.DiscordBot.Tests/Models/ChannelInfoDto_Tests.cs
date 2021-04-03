namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models.Dtos;

    public class ChannelInfoDto_Tests
    {
        [Fact]
        public void BasicChannelInfoDto()
        {
            var cidto = new ChannelInfoDto();
            Assert.NotNull(cidto);
        }
    }
}
