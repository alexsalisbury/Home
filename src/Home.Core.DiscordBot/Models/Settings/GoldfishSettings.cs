namespace Home.Core.DiscordBot.Models.Settings
{
    public record GoldfishSettings
    {
        public uint Cadence { get; init; }
        public bool Silent { get; init; }
        public string Strategy { get; init; }
    }
}
