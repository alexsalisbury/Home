namespace Home.Core.Tests.Mocks
{
    using System.Threading.Tasks;
    using Home.Core.Interfaces;

    public class MockTokenService : IAcquireTokenService
    {
        public string Result { get; set; }

        public async Task<string> GetBearerTokenHeaderAsync()
        {
            await Task.Delay(1);
            return Result;
        }
    }
}