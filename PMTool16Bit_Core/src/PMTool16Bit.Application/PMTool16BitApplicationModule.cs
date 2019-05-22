using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PMTool16Bit.Authorization;

namespace PMTool16Bit
{
    [DependsOn(
        typeof(PMTool16BitCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class PMTool16BitApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<PMTool16BitAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(PMTool16BitApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
