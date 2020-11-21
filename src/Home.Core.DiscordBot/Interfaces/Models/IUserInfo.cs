namespace Home.Core.DiscordBot.Interfaces.Models
{
    using Discord;

    public interface IUserInfo : ISnowflakeEntity
    {
        int ShyId { get; }
        string Username { get; }
        string DiscriminatorValue { get; }
        bool IsBot { get; }
        bool IsWebhook { get; }
        string AvatarId { get; }
    }
}
