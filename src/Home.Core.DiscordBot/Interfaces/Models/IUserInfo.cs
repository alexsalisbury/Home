namespace Home.Core.DiscordBot.Interfaces.Models
{
    using Discord;
    using Home.Core.Interfaces.Models;

    public interface IUserInfo : IShyEntity, ISnowflakeEntity
    {
        string Username { get; }
        string DiscriminatorValue { get; }
        bool IsBot { get; }
        bool IsWebhook { get; }
        string AvatarId { get; }
    }
}
