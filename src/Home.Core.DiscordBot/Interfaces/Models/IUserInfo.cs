namespace Home.Core.DiscordBot.Interfaces.Models
{
    using Discord;

    public interface IUserInfo : ISnowflakeEntity
    {
        string Username { get; }
        ushort DiscriminatorValue { get; }
        bool IsBot { get; }
        bool IsWebhook { get; }
        string AvatarId { get; }
    }
}
