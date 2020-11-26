namespace Home.Core.DiscordBot.Interfaces.Models
{
    using Discord;
    using Home.Core.Interfaces.Models;

    public interface IMessageInfo : IShyEntity, ISnowflakeEntity
    {
        ulong AuthorId { get; }
        ulong ChannelId { get; }
        ulong? GuildId { get; }
        bool IsPinned { get; }

        // TODO: Reactions - seperate interface?
        // TODO: IContentHolder interface
    }
}
