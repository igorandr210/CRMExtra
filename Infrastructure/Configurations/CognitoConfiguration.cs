
namespace Infrastructure.Configurations
{
    public class CognitoConfiguration
    {
        public const string ConfigurationSection = "AWS";
        
        public string Region { get; init; }
        public string UserPoolId { get; init; }
        public string UserPoolClientId { get; init; }
        public string UserPoolClientSecret { get; init; }
    }
}