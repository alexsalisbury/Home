namespace Home.Core.Models.Settings
{
    using System.Diagnostics;

    public record AzureSettings
    {
        /// <summary>
        /// The sts authority for this app.
        /// </summary>
        [DebuggerDisplay("SECRET")]
        public string Authority => $"https://login.microsoftonline.com/{this.TenantId}/";

        [DebuggerDisplay("SECRET")]
        public string ShyCloudEndpoint { get; init; }

        [DebuggerDisplay("SECRET")]
        public string ClientId { get; init; }
        [DebuggerDisplay("SECRET")]
        public string ClientSecret { get; init; }
        [DebuggerDisplay("SECRET")]
        public string KVToken { get; init; }
        [DebuggerDisplay("SECRET")]
        public string Instance { get; init; }
        [DebuggerDisplay("SECRET")]
        public string Scope { get; init; }
        [DebuggerDisplay("SECRET")]
        public string TenantId { get; init; }
        [DebuggerDisplay("SECRET")]
        public string VaultName { get; init; }
        [DebuggerDisplay("SECRET")]
        public string FunctionQueue { get; init; }
        [DebuggerDisplay("SECRET")]
        public string ServiceBusConnectionString { get; init; }
        [DebuggerDisplay("SECRET")]
        public string SelfQueue { get; init; }
    }
}