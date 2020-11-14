namespace Home.Core.DiscordBot.Interfaces.Models
{
    using Discord;

    public enum ChannelType
    {
        GuildText,
        DM,
        GuildVoice,
        GroupDM,
        GuildCategory,
        GuildNews,
        GuildStore
    }

    public interface IChannelInfo : ISnowflakeEntity
    {
        ulong? CategoryId { get; }
        ulong? GuildId { get; }
        bool IsNsfw { get; }
        int Position { get; }

        //TODO: IEnumerable<IUserInfo> Users;
        //TODO: IEnumerable<IChannelInfo> Channels;
    }
}
