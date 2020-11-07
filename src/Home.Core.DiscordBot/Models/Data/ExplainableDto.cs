﻿namespace Home.Core.DiscordBot.Models.Dtos
{
    using Home.Core.DiscordBot.Interfaces;

    public class ExplainableDto : IExplainable
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Explanation { get; set; }
    }
}