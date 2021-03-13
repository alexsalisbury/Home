namespace Home.Core.Interfaces.Settings
{
    using System.Diagnostics;

    /// <summary>
    /// Settings related to Azure Key Vault
    /// </summary>
    public interface IAzureSettings
    {
        string Authority { get; }
        string ShyCloudEndpoint { get; init; }

        /// <summary>
        /// The client/app guid for this bot.
        /// </summary>
        [DebuggerDisplay("SECRET")]
        string ClientId { get; init; }

        /// <summary>
        /// The client/app guid for this bot.
        /// </summary>
        [DebuggerDisplay("SECRET")]
        string ClientSecret { get; init; }

        /// <summary>
        /// The client/app guid for this bot.
        /// </summary>
        [DebuggerDisplay("SECRET")]
        string Scope { get; init; }

        /// <summary>
        /// The KV Token
        /// </summary>
        [DebuggerDisplay("SECRET")]
        string KVToken { get; init; }

        /// <summary>
        /// The tenant guid for this app.
        /// </summary>
        [DebuggerDisplay("SECRET")]
        string TenantId { get; init; }

        /// <summary>
        /// The vault name to connect to.
        /// </summary>
        [DebuggerDisplay("SECRET")]
        string VaultName { get; init; }
    }
}
