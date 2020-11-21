namespace Home.Core.DiscordBot.Interfaces.Models
{
    using Discord;

    public interface IMessageInfo : ISnowflakeEntity
    {
        int ShyId { get; }
        ulong AuthorId { get; }
        ulong ChannelId { get; }
        ulong? GuildId { get; }
        bool IsPinned { get; }

        // TODO: Reactions - seperate interface?
        // TODO: IContentHolder interface
    }
}
