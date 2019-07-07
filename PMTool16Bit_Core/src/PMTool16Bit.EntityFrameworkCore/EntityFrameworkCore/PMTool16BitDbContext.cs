using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Models;
using PMTool16Bit.MultiTenancy;

namespace PMTool16Bit.EntityFrameworkCore
{
    public class PMTool16BitDbContext : AbpZeroDbContext<Tenant, Role, User, PMTool16BitDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<EventTask> EvenTasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<EventTaskMember> EventTaskMembers { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<ProjectActivity> ProjectActivities { get; set; }
        public DbSet<FileEntity> Files { get; set; }

        public PMTool16BitDbContext(DbContextOptions<PMTool16BitDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(c => c.TaskGroups)
                .WithOne(e => e.Project)
                /*.OnDelete(DeleteBehavior.Cascade); */;

            modelBuilder.Entity<TaskGroup>()
               .HasMany(c => c.EventTasks)
               .WithOne(e => e.TaskGroup)
               /*.OnDelete(DeleteBehavior.Cascade); */;

            modelBuilder.Entity<EventTask>()
              .HasMany(c => c.Comments)
              .WithOne(e => e.EventTask)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventTask>()
              .HasMany(c => c.Todos)
              .WithOne(e => e.EventTask)
              .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity<EventTaskMember>()
             .HasKey(bc => new { bc.EventTaskId, bc.MemberId });

            modelBuilder.Entity<EventTaskMember>()
                .HasOne(bc => bc.EventTask)
                .WithMany(c => c.EventTaskMembers)
                .HasForeignKey(bc => bc.EventTaskId);

            modelBuilder.Entity<EventTaskMember>()
              .HasOne(bc => bc.Member)
              .WithMany(c => c.EventTaskMembers)
              .HasForeignKey(bc => bc.MemberId);

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