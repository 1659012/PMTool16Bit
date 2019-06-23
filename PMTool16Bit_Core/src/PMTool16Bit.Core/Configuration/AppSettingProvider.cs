using System.Collections.Generic;
using Abp.Configuration;
using PMTool16Bit.Models.Enum;

namespace PMTool16Bit.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(SettingKey.App_BaseUrl, "http://localhost:21021/", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.App_FrontEndBaseUrl, "http://localhost:8080/", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
            };
        }
    }
}
