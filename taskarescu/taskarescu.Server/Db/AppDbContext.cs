using taskarescu.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace taskarescu.Server.Db
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            // Set composite primary keys
            modelBuilder.Entity<UserBadge>()
                .HasKey(ub => new { ub.UserId, ub.BadgeId });
            modelBuilder.Entity<StudentProject>()
                .HasKey(ps => new { ps.UserId, ps.ProjectId });

            // Set unique constraints
            //modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            //modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            //modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<Badge>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<Status>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Difficulty>().HasIndex(d => d.Name).IsUnique();

            // UserBadge and Badge relationship
            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.Badge)
                .WithMany(b => b.UserBadges)
                .HasForeignKey(ub => ub.BadgeId);

            // UserBadge and User relationship
            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.UserId);

            // StudentProject and User relationship
            modelBuilder.Entity<StudentProject>()
                .HasOne(sp => sp.User)
                .WithMany(u => u.StudentProjects)
                .HasForeignKey(sp => sp.UserId);

            // StudentProject and Project relationship
            modelBuilder.Entity<StudentProject>()
                .HasOne(sp => sp.Project)
                .WithMany(p => p.StudentProjects)
                .HasForeignKey(sp => sp.ProjectId);

            // Feedback and User relationship
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedback)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // User and Project relationship
            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }

        //public override DbSet<User> Users { get; set; }
        //public override DbSet<Role> Roles { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<StudentProject> StudentProjects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}