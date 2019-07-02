using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using PMTool16Bit.Configuration;
using PMTool16Bit.EntityFrameworkCore;
using PMTool16Bit.Migrator.DependencyInjection;

namespace PMTool16Bit.Migrator
{
    [DependsOn(typeof(PMTool16BitEntityFrameworkModule))]
    public class PMTool16BitMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public PMTool16BitMigratorModule(PMTool16BitEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(PMTool16BitMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                PMTool16BitConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus),
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PMTool16BitMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}