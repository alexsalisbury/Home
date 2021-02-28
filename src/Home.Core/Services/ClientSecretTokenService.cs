namespace Home.Core.Services
{
    using System;
    using Microsoft.Identity.Client;
    using Home.Core.Interfaces.Settings;

    public class ClientSecretTokenService : IdentityClientTokenService
    {
        public ClientSecretTokenService(IAzureSettings settings) : base(settings)
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
