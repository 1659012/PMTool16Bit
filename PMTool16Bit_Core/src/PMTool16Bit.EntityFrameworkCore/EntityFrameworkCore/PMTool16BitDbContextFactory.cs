using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PMTool16Bit.Configuration;
using PMTool16Bit.Web;

namespace PMTool16Bit.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class PMTool16BitDbContextFactory : IDesignTimeDbContextFactory<PMTool16BitDbContext>
    {
        public PMTool16BitDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PMTool16BitDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            PMTool16BitDbContextConfigurer.Configure(builder, configuration.GetConnectionString(PMTool16BitConsts.ConnectionStringName));

            return new PMTool16BitDbContext(builder.Options);
        }
    }
}
