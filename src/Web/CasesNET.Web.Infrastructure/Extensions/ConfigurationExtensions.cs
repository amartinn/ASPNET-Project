namespace CasesNET.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString("DefaultConnection");

        public static string GetThirdPartyAppId(this IConfiguration configuration, string providerName)
            => configuration[$"ThirdPartyLogins:{providerName}:AppId"];

        public static string GetThirdPartyAppSecret(this IConfiguration configuration, string providerName)
            => configuration[$"ThirdPartyLogins:{providerName}:AppSecret"];
    }
}
