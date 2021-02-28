namespace Home.Core.Windows.Clients
{
    using Home.Core.Clients;
    using Home.Core.Interfaces.Settings;
    using Home.Core.Services;

    public abstract class HomeCertClient : HomeClient
    {
        protected HomeCertClient(IAzureSettings settings, ClientSecretTokenService tokenService) : base(settings, tokenService)
        {
        }
    }
}