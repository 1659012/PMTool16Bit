using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Editions;

namespace PMTool16Bit.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore)
            : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager,
                featureValueStore)
        {
        }
    }
}