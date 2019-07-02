using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PMTool16Bit.EntityFrameworkCore
{
    public static class PMTool16BitDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PMTool16BitDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PMTool16BitDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}