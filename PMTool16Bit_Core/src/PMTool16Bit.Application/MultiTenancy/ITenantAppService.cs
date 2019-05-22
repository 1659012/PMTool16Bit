using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PMTool16Bit.MultiTenancy.Dto;

namespace PMTool16Bit.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

