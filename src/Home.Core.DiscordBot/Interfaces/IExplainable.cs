namespace Home.Core.DiscordBot.Interfaces
{
    public interface IExplainable
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Explanation { get; set; }
    }
}
