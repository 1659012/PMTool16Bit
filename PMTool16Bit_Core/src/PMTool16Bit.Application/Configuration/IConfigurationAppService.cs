using PMTool16Bit.Configuration.Dto;
using System.Threading.Tasks;

namespace PMTool16Bit.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}