using System.Threading.Tasks;
using Abp.Application.Services;
using PMTool16Bit.Authorization.Accounts.Dto;

namespace PMTool16Bit.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
