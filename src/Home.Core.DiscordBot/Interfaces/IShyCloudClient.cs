namespace Home.Core.DiscordBot.Interfaces.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Home.Core.DiscordBot.Interfaces.Models;

    public interface IShyCloudClient
    {
        Task<IEnumerable<IExplainable>> FetchExplainablesAsync();
        Task UploadChannelAsync(IChannelInfo dto);
    }
}
