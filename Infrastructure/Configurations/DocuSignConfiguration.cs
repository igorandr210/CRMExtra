namespace Infrastructure.Configurations
{
    public class DocuSignConfiguration
    {
        public const string ConfigurationSection = "DocuSignConfiguration";
        public string BasePath { get; init; }
        public string PrivateKey { get; init; }
        public string AuthServer { get; init; }
        public string IntegrationKey { get; init; }
        public string TemplateId { get; init; }
        public string RedirectUrl { get; init; }
        public string SecretKey { get; init; }
        public string DocuSignUserId { get; init; }
    }
}
