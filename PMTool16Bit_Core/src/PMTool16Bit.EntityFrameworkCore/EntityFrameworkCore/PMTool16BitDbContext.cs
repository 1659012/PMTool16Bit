using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.MultiTenancy;

namespace PMTool16Bit.EntityFrameworkCore
{
    public class PMTool16BitDbContext : AbpZeroDbContext<Tenant, Role, User, PMTool16BitDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public PMTool16BitDbContext(DbContextOptions<PMTool16BitDbContext> options)
            : base(options)
        {
        }
    }
}
