namespace Home.Core.DiscordBot.Models.Dtos
{
    using System;
    using Home.Core.DiscordBot.Interfaces.Models;

    public record ChannelInfoDto : IChannelInfo
    {
        public ulong? CategoryId { get; init; }

        public ulong? GuildId { get; init; }

        public bool IsNsfw { get; init; }

        public int Position { get; init; }

        public DateTimeOffset CreatedAt { get; init; }

        public ulong Id { get; init; }
    }
}