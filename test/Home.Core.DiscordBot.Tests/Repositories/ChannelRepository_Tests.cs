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
        public async Task FetchSingleTest()
        {
            int count = 1;
            var expected = GetDefaults(count);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var cr = new ChannelRepository(connection.Object);
            var result = await cr.FetchAsync();
            Assert.NotNull(result);
            Assert.Equal(typeof(ChannelInfoDto), result.First().GetType());
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task FetchTest()
        {
            int count = 3;
            var expected = GetDefaults(count);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var cr = new ChannelRepository(connection.Object);
            var result = await cr.FetchAsync();
            Assert.NotNull(result);
            Assert.Equal(typeof(ChannelInfoDto), result.First().GetType());
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task FetchByIdTest()
        {
            int count = 3;
            var expected = GetDefaults(count);
            ulong id = expected.First().Id;

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var cr = new ChannelRepository(connection.Object);
            var result = await cr.FetchAsync(id);
            Assert.NotNull(result);
            Assert.Equal(typeof(ChannelInfoDto), result.GetType());
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task FetchByIdNotFoundTest()
        {
            int count = 0;
            var expected = GetDefaults(count);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var cr = new ChannelRepository(connection.Object);
            var result = await cr.FetchAsync(7);
            Assert.Null(result);
        }

        private IEnumerable<ChannelInfoDto> GetDefaults(int count = 3)
        {
            var result = new List<ChannelInfoDto>();
            while (count > 0)
            {
                result.Add(ModelGenerator.GenerateChannelInfoDto());
                count -= 1;
            }

            return result;
        }
    }
}
