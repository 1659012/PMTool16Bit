using System.Threading.Tasks;
using Abp.Application.Services;
using PMTool16Bit.Sessions.Dto;

namespace PMTool16Bit.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
