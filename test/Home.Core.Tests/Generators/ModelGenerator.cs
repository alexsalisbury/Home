namespace Home.Core.Tests.Generators
{
    using System;
    using Bogus;
    using Home.Core.Models.Settings;

    public static class ModelGenerator
    {
        static Faker<AzureSettings> azureSettingsFaker;

        static ModelGenerator()
        {
            azureSettingsFaker = azureSettingsFaker ?? new Faker<AzureSettings>()
                .RuleFor(az => az.ClientId, (f, u) => Guid.NewGuid().ToString())
                .RuleFor(az => az.ClientSecret, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(az => az.FunctionQueue, (f, u) => Guid.NewGuid().ToString())
                .RuleFor(az => az.Instance, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(az => az.KVToken, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(az => az.Scope, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(az => az.SelfQueue, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(az => az.ServiceBusConnectionString, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(az => az.ShyCloudEndpoint, (f, u) => f.Random.AlphaNumeric(20))
                .RuleFor(az => az.TenantId, (f, u) => Guid.NewGuid().ToString())
                .RuleFor(az => az.VaultName, (f, u) => f.Random.AlphaNumeric(20));
        }

        public static AzureSettings GenerateAzureSettings()
        {
            return azureSettingsFaker.Generate();
        }
    }
}
