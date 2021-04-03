namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models.Dtos;

    public class MessageInfoDto_Tests
    {
        [Fact]
        public void BasicMessageInfoDto()
        {
            var midto = new MessageInfoDto();
            Assert.NotNull(midto);
        }
    }
}
