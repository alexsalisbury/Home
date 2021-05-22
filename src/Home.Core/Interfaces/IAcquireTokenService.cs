namespace Home.Core.Interfaces
{
    using System.Threading.Tasks;

    public interface IAcquireTokenService
    {
        abstract Task<string> GetBearerTokenHeaderAsync();
    }
}
