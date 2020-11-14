namespace Home.Core.DiscordBot.Models.Dtos
{
    using System;
    using Home.Core.DiscordBot.Interfaces.Models;

    public class ChannelInfoDto : IChannelInfo
    {
        public ulong? CategoryId { get; }

        public ulong? GuildId { get; }

        public bool IsNsfw { get; }

        public int Position { get; }

        public DateTimeOffset CreatedAt { get; }

        public ulong Id { get; }
    }
}