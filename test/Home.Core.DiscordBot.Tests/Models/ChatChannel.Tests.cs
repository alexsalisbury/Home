namespace Home.Core.DiscordBot.Tests.Models
{
    using System;
    using Xunit;
    using Home.Core.DiscordBot.Models;
    using Home.Core.DiscordBot.Models.Dtos;

    public class ChatChannel_Tests
    {
        [Fact]
        public void BasicChatChannel()
        {
            var cc = new ChatChannel(0, string.Empty, null);
            Assert.NotNull(cc);
        }

        [Fact]
        public void PopulatedChatChannel()
        {
            var cc = new ChatChannel(1, "Mock", "General");
            var cache = cc?.DtoCache;
            Assert.NotNull(cache);
            Assert.Equal<ulong>(1, cache.Id);
            Assert.Equal("Mock", cache.Codeword);
            Assert.Equal("General", cache.Name);
        }

        [Fact]
        public void PopulatedCache()
        {
            var ci = new ChannelInfoDto()
            {
                CategoryId = 3,
                Codeword = "Mock",
                CreatedAt = DateTime.UtcNow,
                GuildId = 1,
                Id = 2,
                IsNsfw = true,
                IsShyRpgChannel = true,
                IsUserDM = false,
                Name = "Fake",
                Position = 4,
                ServerShyId = 5,
                ShyId = 6
            };

            var cc = new ChatChannel(ci);
            var cache = (ChannelInfoDto)cc?.DtoCache;
            Assert.NotNull(cache);
            Assert.Equal(ci.CategoryId, cache.CategoryId);
            Assert.Equal(ci.Codeword, cache.Codeword);
            Assert.Equal(ci.CreatedAt, cache.CreatedAt);
            Assert.Equal(ci.GuildId, cache.GuildId);
            Assert.Equal(ci.Name, cache.Name);
            Assert.Equal(ci.Position, cache.Position);
            Assert.Equal(ci.ServerShyId, cache.ServerShyId);
            Assert.Equal(ci.ShyId, cache.ShyId);
            Assert.Equal(ci.Id, cache.Id);
            Assert.True(cache.IsNsfw);
            Assert.True(cache.IsShyRpgChannel);
            Assert.False(cache.IsUserDM);
        }
    }
}
