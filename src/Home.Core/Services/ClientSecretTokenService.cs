namespace Home.Core.Services
{
    using System;
    using Microsoft.Identity.Client;
    using Home.Core.Models.Settings;

    public class ClientSecretTokenService : IdentityClientTokenService
    {
        public ClientSecretTokenService(AzureSettings settings) : base(settings)
        {
        }

        protected override IConfidentialClientApplication GetBuilder()
        {
            return ConfidentialClientApplicationBuilder.Create(settings.ClientId)
                    .WithClientSecret(settings.ClientSecret)
                    .WithAuthority(new Uri(settings.Authority))
                    .Build();
        }
    }
}
