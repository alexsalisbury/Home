namespace Home.Core.DiscordBot.Models.Settings
{
    using System.Collections.Generic;

    /// <summary>
    /// The settings for a channel
    /// </summary>
    public class ChannelSettings
    {
        /// <summary>
        /// The name of the channel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Goldfish settings, if any
        /// </summary>
        public GoldfishSettings Fish { get; set; }

        /// <summary>
        /// Default json ctor.
        /// </summary>
        public ChannelSettings()
        {
        }

        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="name">The name of the channel</param>
        /// <param name="fish">The fish settings if any.</param>
        public ChannelSettings(string name, GoldfishSettings fish = null)
        {
            this.Name = name;
            this.Fish = fish;
        }

        public IEnumerable<string> GetAbout()
        {
            var silent = (this.Fish.Silent ? "SILENT" : string.Empty);
            yield return $"Fish : {this.Fish.Strategy}-{this.Fish.Cadence} {silent}";
        }
    }
}
