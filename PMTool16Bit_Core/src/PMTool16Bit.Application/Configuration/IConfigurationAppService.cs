using System.Threading.Tasks;
using PMTool16Bit.Configuration.Dto;

namespace PMTool16Bit.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
