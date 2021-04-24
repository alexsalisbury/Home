namespace Home.Core.DiscordBot.Models
{
    using System.Collections.Generic;
    using Discord;
    using Serilog;
    using Home.Core.DiscordBot.Interfaces.Services;
    using Home.Core.DiscordBot.Models.Settings;

    public class ListenerServer : Server, IListenerServer
    {
        public ListenerServer(ServerInfo server)// : base(server)
        {
        }

        public async IAsyncEnumerable<IMessage> CaptureMessagesAsync()
        {
            yield return null;
        }
            //        var channels = this.guild.TextChannels;
            //        foreach (var current in channels)
            //        {
            //            Log.Information("Crawling messages for {channelCategoryName} {channelName}", current?.Category?.Name ?? "", current?.Name ?? "");
            //            var itemshandled = 0;
            //            var setshandled = 0;

            //            var messageCollection = current.GetMessagesAsync(1000);
            //            await foreach (var messageSet in messageCollection)
            //            {
            //                setshandled += 1;
            //                foreach (var msg in messageSet)
            //                {
            //                    itemshandled += 1;
            //                    yield return msg;
            //                }
            //            }

            //            if (itemshandled > 0)
            //            {
            //                Log.Information("Channel {channelCategoryName}:{channelName} has {itemshandled} messages in {setshandled} sets.", current?.Category?.Name ?? "", current?.Name ?? "", itemshandled, setshandled);
            //            }
            //        }
            //    }

            //}

            ////////{
            ////////    public ConcurrentDictionary<ulong, string> MessageHashCache = new ConcurrentDictionary<ulong, string>();
            ////////    /// <summary>
            ////////    /// The Channels on this server.
            ////////    /// </summary>
            ////////    public ConcurrentDictionary<string, DiscordChannel> Channels { get; private set; } = new ConcurrentDictionary<string, DiscordChannel>();

            ////////    public string Codeword { get; set; }

            ////////    /// <summary>
            ////////    /// The Server ID of this server.
            ////////    /// </summary>
            ////////    public ulong ServerId { get; set; }

            ////////    private SocketGuild guild;
            ////////    private IEnumerable<ChannelSettings> channelSettings;

            ////////    /// <summary>
            ////////    /// Constructs a Server mock.
            ////////    /// </summary>
            ////////    /// <param name="settings">The settings of this server.</param>
            ////////    public Server(ServerInfo server, IShyCloudClient client, IDiscordClient discord)
            ////////    {
            ////////        if (Guilds.ContainsKey(server.Codeword))
            ////////        {
            ////////            Log.Warning("Duplicate codeword {serverCodeword} found.", server.Codeword);
            ////////        }

            ////////        this.ServerId = server.ServerId;
            ////////        this.Codeword = server.Codeword;
            ////////        channelSettings = server.Channels;
            ////////        Guilds[server.Codeword] = this;
            ////////    }


            ////////    internal async Task<bool> StartArchiveAsync()
            ////////    {
            ////////        var archiveCommand = new ArchiveCommand(this.Codeword);
            ////////        //Enqueue(archiveCommand);
            ////////        //var stage = archiveCommand.GetNextStage(); //Still in planning. Not sure about this.
            ////////        var result =  await archiveCommand.ExecuteCommandStageAsync();
            ////////        return true;
            ////////    }
        }
}