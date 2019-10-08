namespace PMTool16Bit.Services
{
    using Abp.Application.Services;
    using Microsoft.EntityFrameworkCore;

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }
    }
}
