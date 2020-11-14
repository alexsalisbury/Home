namespace Home.Core.DiscordBot.Models.Dtos
{
    using System;
    using Home.Core.DiscordBot.Interfaces.Models;

    public class UserInfoDto : IUserInfo
    {
        public string Username { get; }

        public ushort DiscriminatorValue { get; }

        public bool IsBot { get; }

        public bool IsWebhook { get; }

        public string AvatarId { get; }

        public DateTimeOffset CreatedAt { get; }

        public ulong Id { get; }
    }
}