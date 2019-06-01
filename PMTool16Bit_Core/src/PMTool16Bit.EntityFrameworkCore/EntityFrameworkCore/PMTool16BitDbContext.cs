using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.MultiTenancy;
using PMTool16Bit.Models;

namespace PMTool16Bit.EntityFrameworkCore
{
    public class PMTool16BitDbContext : AbpZeroDbContext<Tenant, Role, User, PMTool16BitDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Project> Projects { get; set; }
        public DbSet<EventTable> EventTables { get; set; }
        public DbSet<EvenTask> EvenTasks { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }

        public PMTool16BitDbContext(DbContextOptions<PMTool16BitDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(c => c.EventTables)
                .WithOne(e => e.Project)
                /*.OnDelete(DeleteBehavior.Cascade); */;

            modelBuilder.Entity<Project>()
               .HasMany(c => c.EventTables)
               .WithOne(e => e.Project)
               /*.OnDelete(DeleteBehavior.Cascade); */;

            modelBuilder.Entity<ProjectMember>()
                .HasKey(bc => new { bc.ProjectId, bc.MemberId });

            modelBuilder.Entity<ProjectMember>()
                .HasOne(bc => bc.Member)
                .WithMany(b => b.ProjectMembers)
                .HasForeignKey(bc => bc.MemberId);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(bc => bc.Project)
                .WithMany(c => c.ProjectMembers)
                .HasForeignKey(bc => bc.ProjectId);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies()
            //    .UseLazyLoadingProxies()
            //    .UseSqlServer(myConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
