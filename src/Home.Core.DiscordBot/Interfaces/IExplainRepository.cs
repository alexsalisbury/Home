namespace Home.Core.DiscordBot.Interfaces.Repositories
{
    using Home.Core.Interfaces.Data.Actions;

    /// <summary>
    /// Repo for IExplainables
    /// </summary>
    /// <remarks>Use of ExplainableDto is recommended</remarks>
    public interface IExplainRepository : IFetchable<IExplainable>
    {
    }
}