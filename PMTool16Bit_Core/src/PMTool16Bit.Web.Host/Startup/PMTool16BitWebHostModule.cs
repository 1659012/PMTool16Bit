using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PMTool16Bit.Configuration;

namespace PMTool16Bit.Web.Host.Startup
{
    [DependsOn(
       typeof(PMTool16BitWebCoreModule))]
    public class PMTool16BitWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PMTool16BitWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PMTool16BitWebHostModule).GetAssembly());
        }
    }
}
