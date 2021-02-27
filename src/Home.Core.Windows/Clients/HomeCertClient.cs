namespace Home.Core.Windows.Clients
{
    using Home.Core.Clients;
    using Home.Core.Models.Settings;
    using Home.Core.Services;

    public abstract class HomeCertClient : HomeClient
    {
        protected HomeCertClient(AzureSettings settings, ClientSecretTokenService tokenService) : base(settings, tokenService)
        {
        }
    }
}