namespace Home.Core.DiscordBot.Models.Dtos
{
    using System;
    using Home.Core.DiscordBot.Interfaces.Models;

    public record ChannelInfoDto : IChannelInfo
    {
        public int ShyId { get; init; }

        public int ServerShyId { get; init; }

        public ulong Id { get; init; }

        public ulong? CategoryId { get; init; }

        public ulong? GuildId { get; init; }

        public bool IsNsfw { get; init; }

        public bool IsShyRpgChannel { get; init; }

        public bool IsUserDM { get; init; }

        public int Position { get; init; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}