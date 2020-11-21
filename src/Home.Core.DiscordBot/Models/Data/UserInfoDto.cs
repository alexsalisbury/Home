namespace Home.Core.DiscordBot.Models.Dtos
{
    using System;
    using Home.Core.DiscordBot.Interfaces.Models;

    public record UserInfoDto : IUserInfo
    {
        public int ShyId { get; init; }

        public ulong Id { get; init; }

        public string Username { get; init; }

        public string DiscriminatorValue { get; init; }

        public bool IsBot { get; init; }

        public bool IsExcluded { get; init; }

        public bool IsWebhook { get; init; }

        public string AvatarId { get; init; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}