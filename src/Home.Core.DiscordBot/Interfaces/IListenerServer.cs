namespace Home.Core.DiscordBot.Interfaces.Services
{
    using System.Collections.Generic;
    using Discord;

    public interface IListenerServer
    {
        IAsyncEnumerable<IMessage> CaptureMessagesAsync();
    }
}
