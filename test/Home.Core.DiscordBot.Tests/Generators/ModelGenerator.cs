namespace Home.Core.DiscordBot.Tests.Generators
{
    using System;
    using Bogus;
    using Home.Core.DiscordBot.Models.Dtos;

    public static class ModelGenerator
    {
        static Faker<ChannelInfoDto> channelFaker;
        static Faker<MessageInfoDto> messageFaker;
        static Faker<UserInfoDto> userFaker;

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

            messageFaker = messageFaker ?? new Faker<MessageInfoDto>()
                .RuleFor(mi => mi.AuthorId, (f, u) => f.Random.ULong())
                .RuleFor(mi => mi.ChannelId, (f, u) => f.Random.ULong())
                .RuleFor(mi => mi.CreatedAt, (f, u) => DateTime.UtcNow)
                .RuleFor(mi => mi.ChannelShyId, (f, u) => f.Random.Int())
                .RuleFor(mi => mi.GuildId, (f, u) => f.Random.ULong())
                .RuleFor(mi => mi.Id, (f, u) => f.Random.ULong())
                .RuleFor(mi => mi.IsPinned, (f, u) => false)
                .RuleFor(mi => mi.ServerShyId, (f, u) => f.Random.Int())
                .RuleFor(mi => mi.ShyId, (f, u) => f.Random.Int(0));

            userFaker = userFaker ?? new Faker<UserInfoDto>()
                .RuleFor(ui => ui.AvatarId, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(ui => ui.CreatedAt, (f, u) => DateTime.UtcNow)
                .RuleFor(ui => ui.DiscriminatorValue, (f, u) => f.Random.Number(9999).ToString())
                .RuleFor(ui => ui.Id, (f, u) => f.Random.ULong())
                .RuleFor(ui => ui.IsBot, (f, u) => false)
                .RuleFor(ui => ui.IsExcluded, (f, u) => false)
                .RuleFor(ui => ui.IsWebhook, (f, u) => false)
                .RuleFor(ui => ui.ShyId, (f, u) => f.Random.Int(0))
                .RuleFor(ui => ui.Username, (f, u) => f.Random.AlphaNumeric(20));
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

        internal static MessageInfoDto GenerateMessageInfoDto()
        {
            return messageFaker.Generate();
        }

        internal static UserInfoDto GenerateUserInfoDto()
        {
            return userFaker.Generate();
        }
    }
}
