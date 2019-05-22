using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using PMTool16Bit.Configuration.Dto;

namespace PMTool16Bit.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : PMTool16BitAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
