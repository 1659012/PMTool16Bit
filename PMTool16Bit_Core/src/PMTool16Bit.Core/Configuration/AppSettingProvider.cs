using Abp.Configuration;
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;

namespace PMTool16Bit.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(SettingKey.App_BaseUrl, "https://pmtool16bitapi.azurewebsites.net", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
                new SettingDefinition(SettingKey.App_FrontEndBaseUrl, "https://pmtool16bit.azurewebsites.net", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false),
            };
        }
    }
}