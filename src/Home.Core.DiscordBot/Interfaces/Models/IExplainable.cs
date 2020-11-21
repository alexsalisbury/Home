namespace Home.Core.DiscordBot.Interfaces.Models
{
    public interface IExplainable
    {
        int ShyId { get; }
        string Subject { get; }
        string Explanation { get; }
    }
}
