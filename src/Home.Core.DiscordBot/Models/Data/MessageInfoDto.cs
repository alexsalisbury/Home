namespace Home.Core.DiscordBot.Models.Dtos
{
    using System;
    using Home.Core.DiscordBot.Interfaces.Models;

    public record MessageInfoDto : IMessageInfo
    {
        public int ShyId { get; init; }

        public ulong Id { get; init; }

        public ulong AuthorId { get; init; }

        public ulong ChannelId { get; init; }

        public ulong? GuildId { get; init; }

        public bool IsPinned { get; init; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}