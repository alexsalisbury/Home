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
        public string ShyCloudEndpoint { get; set; }

        [DebuggerDisplay("SECRET")]
        public string ClientId { get; set; }
        [DebuggerDisplay("SECRET")]
        public string ClientSecret { get; set; }
        [DebuggerDisplay("SECRET")]
        public string KVToken { get; set; }
        [DebuggerDisplay("SECRET")]
        public string Instance { get; set; }
        [DebuggerDisplay("SECRET")]
        public string Scope { get; set; }
        [DebuggerDisplay("SECRET")]
        public string TenantId { get; set; }
        [DebuggerDisplay("SECRET")]
        public string VaultName { get; set; }
        [DebuggerDisplay("SECRET")]
        public string FunctionQueue { get; set; }
        [DebuggerDisplay("SECRET")]
        public string ServiceBusConnectionString { get; set; }
        [DebuggerDisplay("SECRET")]
        public string SelfQueue { get; set; }
    }
}