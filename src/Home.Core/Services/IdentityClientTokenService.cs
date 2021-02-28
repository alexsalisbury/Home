namespace Home.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Identity.Client;
    using Home.Core.Interfaces;
    using Home.Core.Interfaces.Settings;

    public abstract class IdentityClientTokenService : IAcquireTokenService
    {
        protected IAzureSettings settings;
        protected List<string> scopes;

        protected IdentityClientTokenService(IAzureSettings settings)
        {
            this.settings = settings;
            this.scopes = settings == null ? new List<string>() : new List<string>() { settings.Scope } ;
        }

        public async Task<string> GetTokenHeader()
        {
            var app = GetBuilder();
            var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            return result.CreateAuthorizationHeader();
        }

        protected abstract IConfidentialClientApplication GetBuilder();
    }
}
