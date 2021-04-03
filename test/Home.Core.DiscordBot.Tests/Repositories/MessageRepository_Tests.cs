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

    public class MessageRepository_Tests
    {
        [Fact]
        public void BasicRepoCtorTest()
        {
            var mr = new MessageRepository(string.Empty);
            Assert.NotNull(mr);
        }

        [Fact]
        public async Task EnsureTest()
        {
            int count = 1;
            var expected = GetDefaults(count);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), null, null, null, null));
            // connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var mr = new MessageRepository(connection.Object);
            var result = await mr.EnsureAsync(expected.First());
            //Assert.True(result); //TODO: When finish API...
            Assert.False(result);
        }

        [Fact]
        public async Task EnsureNullTest()
        {
            int count = 0;
            var expected = GetDefaults(count);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), null, null, null, null));
            // connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var mr = new MessageRepository(connection.Object);
            var result = await mr.EnsureAsync(expected.FirstOrDefault());
            Assert.False(result);
        }

        //[Fact]
        //public async Task FetchSingleTest()
        //{
        //    int count = 1;
        //    var expected = GetDefaults(count);

        //    var connection = new Mock<DbConnection>();
        //    connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

        //    var mr = new MessageRepository(connection.Object);
        //    var result = await mr.FetchAsync();
        //    Assert.NotNull(result);
        //    Assert.Equal(typeof(ChannelInfoDto), result.First().GetType());
        //    Assert.Equal(count, result.Count());
        //}

        //[Fact]
        //public async Task FetchTest()
        //{
        //    int count = 3;
        //    var expected = GetDefaults(count);

        //    var connection = new Mock<DbConnection>();
        //    connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

        //    var mr = new MessageRepository(connection.Object);
        //    var result = await mr.FetchAsync();
        //    Assert.NotNull(result);
        //    Assert.Equal(typeof(ChannelInfoDto), result.First().GetType());
        //    Assert.Equal(count, result.Count());
        //}

        //[Fact]
        //public async Task FetchByIdTest()
        //{
        //    int count = 3;
        //    var expected = GetDefaults(count);
        //    ulong id = expected.First().Id;

        //    var connection = new Mock<DbConnection>();
        //    connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

        //    var mr = new MessageRepository(connection.Object);
        //    var result = await mr.FetchAsync(id);
        //    Assert.NotNull(result);
        //    Assert.Equal(typeof(ChannelInfoDto), result.GetType());
        //    Assert.Equal(id, result.Id);
        //}

        //[Fact]
        //public async Task FetchByIdNotFoundTest()
        //{
        //    int count = 0;
        //    var expected = GetDefaults(count);

        //    var connection = new Mock<DbConnection>();
        //    connection.SetupDapperAsync(c => c.QueryAsync<ChannelInfoDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

        //    var mr = new MessageRepository(connection.Object);
        //    var result = await mr.FetchAsync(7);
        //    Assert.Null(result);
        //}

        private IEnumerable<MessageInfoDto> GetDefaults(int count = 3)
        {
            var result = new List<MessageInfoDto>();
            while (count > 0)
            {
                result.Add(ModelGenerator.GenerateMessageInfoDto());
                count -= 1;
            }

            return result;
        }
    }
}
