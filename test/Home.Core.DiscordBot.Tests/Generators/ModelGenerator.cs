namespace Home.Core.DiscordBot.Tests.Generators
{
    using System;
    using Bogus;
    using Home.Core.DiscordBot.Models.Dtos;

    public static class ModelGenerator
    {
        static Faker<ChannelInfoDto> channelFaker;

        static ModelGenerator()
        {
            channelFaker = channelFaker ?? new Faker<ChannelInfoDto>()
                .RuleFor(ci => ci.CategoryId, (f, u) => f.Random.ULong())
                .RuleFor(ci => ci.Codeword, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(ci => ci.CreatedAt, (f, u) => DateTime.UtcNow)
                .RuleFor(ci => ci.GuildId, (f, u) => f.Random.ULong())
                .RuleFor(ci => ci.Id, (f, u) => f.Random.ULong())
                .RuleFor(ci => ci.IsNsfw, (f, u) => false)
                .RuleFor(ci => ci.IsShyRpgChannel, (f, u) => false)
                .RuleFor(ci => ci.IsUserDM, (f, u) => false)
                .RuleFor(ci => ci.Name, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(ci => ci.Position, (f, u) => 0)
                .RuleFor(ci => ci.ServerShyId, (f, u) => f.Random.Int(0))
                .RuleFor(ci => ci.ShyId, (f, u) => f.Random.Int(0));
            //azureSettingsFaker = azureSettingsFaker ?? new Faker<AzureSettings>()
            //    .RuleFor(az => az.ClientId, (f, u) => Guid.NewGuid().ToString())
            //    .RuleFor(az => az.ClientSecret, (f, u) => f.Random.AlphaNumeric(20))
            //    .RuleFor(az => az.FunctionQueue, (f, u) => Guid.NewGuid().ToString())
            //    .RuleFor(az => az.Instance, (f, u) => f.Random.AlphaNumeric(20))
            //    .RuleFor(az => az.KVToken, (f, u) => f.Random.AlphaNumeric(20))
            //    .RuleFor(az => az.Scope, (f, u) => f.Random.AlphaNumeric(20))
            //    .RuleFor(az => az.SelfQueue, (f, u) => f.Random.AlphaNumeric(20))
            //    .RuleFor(az => az.ServiceBusConnectionString, (f, u) => f.Random.AlphaNumeric(20))
            //    .RuleFor(az => az.ShyCloudEndpoint, (f, u) => f.Random.AlphaNumeric(20))
            //    .RuleFor(az => az.TenantId, (f, u) => Guid.NewGuid().ToString())
            //    .RuleFor(az => az.VaultName, (f, u) => f.Random.AlphaNumeric(20));
        }

        public static ChannelInfoDto GenerateChannelInfoDto()
        {
            return channelFaker.Generate();
        }
    }
}
