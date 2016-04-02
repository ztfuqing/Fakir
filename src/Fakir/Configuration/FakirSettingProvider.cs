using Abp.Configuration;
using System.Collections.Generic;
using System.Configuration;

namespace Fakir.Configuration
{
    public class FakirSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(FakirSettings.General.WebSiteRootAddress, "http://localhost:12345/"),

                       new SettingDefinition(FakirSettings.UserManagement.AllowSelfRegistration, ConfigurationManager.AppSettings[FakirSettings.UserManagement.AllowSelfRegistration] ?? "true", scopes: SettingScopes.Tenant),
                       new SettingDefinition(FakirSettings.UserManagement.IsNewRegisteredUserActiveByDefault, ConfigurationManager.AppSettings[FakirSettings.UserManagement.IsNewRegisteredUserActiveByDefault] ?? "false", scopes: SettingScopes.Tenant),
                       new SettingDefinition(FakirSettings.UserManagement.UseCaptchaOnRegistration, ConfigurationManager.AppSettings[FakirSettings.UserManagement.UseCaptchaOnRegistration] ?? "true", scopes: SettingScopes.Tenant),
                   };
        }
    }
}
