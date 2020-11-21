namespace Home.Core.Models.Settings
{
    public record AzureSettings
    {
        public string ShyCloudEndpoint { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string KVToken { get; set; }
        public string Instance { get; set; }
        public string Scope { get; set; }
        public string TenantId { get; set; }
        public string VaultName { get; set; }
        public string FunctionQueue { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public string SelfQueue { get; set; }
    }
}