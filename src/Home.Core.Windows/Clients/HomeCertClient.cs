namespace Home.Core.Windows.Clients
{
    using System.Net.Http;
    using Home.Core.Clients;
    using Home.Core.Interfaces.Settings;
    using Home.Core.Services;

    public abstract class HomeCertClient : HomeClient
    {
        protected HomeCertClient(IAzureSettings settings, ClientSecretTokenService tokenService, HttpMessageHandler handler) : base(settings, tokenService, handler)
        {
        }
    }
}