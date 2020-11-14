namespace Home.Core.DiscordBot.Models.Dtos
{
    using System;
    using Home.Core.DiscordBot.Interfaces.Models;

    public class MessageInfoDto : IMessageInfo
    {
        public ulong AuthorId { get; }

        public ulong ChannelId { get; }

        public ulong? GuildId { get; }

        public bool IsPinned { get; }

        public DateTimeOffset CreatedAt { get; }

        public ulong Id { get; }
    }
}