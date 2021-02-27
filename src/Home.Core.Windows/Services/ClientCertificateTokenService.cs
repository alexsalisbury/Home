namespace Home.Core.Windows.Services
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Identity.Client;
    using Home.Core.Models.Settings;
    using Home.Core.Services;

    public class ClientCertificateTokenService : IdentityClientTokenService
    {
        private X509Certificate2 cert;
        public ClientCertificateTokenService(AzureSettings settings, X509Certificate2 cert) : base(settings)
        {
            this.cert = cert;
        }

        protected override IConfidentialClientApplication GetBuilder()
        {
            return ConfidentialClientApplicationBuilder.Create(settings.ClientId)
                .WithCertificate(cert)
                .WithAuthority(new Uri(settings.Authority))
                .Build();
        }
    }
}
