using Abp.Application.Services;
using PMTool16Bit.Sessions.Dto;
using System.Threading.Tasks;

namespace PMTool16Bit.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}