using Abp.Application.Services;
using PMTool16Bit.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace PMTool16Bit.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}