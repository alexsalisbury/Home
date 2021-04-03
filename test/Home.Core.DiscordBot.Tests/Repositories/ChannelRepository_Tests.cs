namespace Home.Core.DiscordBot.Tests.Repositories
{
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Moq;
    using Moq.Dapper;
    using Xunit;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.DiscordBot.Repositories;
    using Home.Core.DiscordBot.Tests.Generators;
    using System.Collections.Generic;

    public class ChannelRepository_Tests
    {
        [Fact]
        public void BasicRepoCtorTest()
        {
            var cr = new ChannelRepository(string.Empty);
            Assert.NotNull(cr);
        }

        [Fact]
        public async Task FetchTest()
        {
            var expected = GetDefaults();

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var cr = new ChannelRepository(connection.Object);
            var result = await cr.FetchAsync();
            Assert.NotNull(result);
            Assert.Equal(typeof(ChannelInfoDto), result.First().GetType());
        }

        private IEnumerable<ChannelInfoDto> GetDefaults()
        {
            var ci = ModelGenerator.GenerateChannelInfoDto();

            return new ChannelInfoDto[] { ci };
        }
    }
}
