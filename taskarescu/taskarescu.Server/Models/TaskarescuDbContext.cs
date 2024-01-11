using Microsoft.EntityFrameworkCore;

namespace taskarescu.Server.Models
{
    public class TaskarescuDbContext : DbContext
    {
        public TaskarescuDbContext(DbContextOptions<TaskarescuDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set composite primary keys
            modelBuilder.Entity<UserBadge>()
                .HasKey(ub => new { ub.UserId, ub.BadgeId });
            modelBuilder.Entity<ProfessorSubject>()
                .HasKey(ps => new { ps.UserId, ps.SubjectId });

            // Set unique constraints
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<Badge>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<Subject>().HasIndex(s => s.Name).IsUnique();
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

            // ProfessorSubject and Subject relationship
            modelBuilder.Entity<ProfessorSubject>()
                .HasOne(ps => ps.Subject)
                .WithMany(s => s.ProfessorSubjects)
                .HasForeignKey(ps => ps.SubjectId);

            // ProfessorSubject and User relationship
            modelBuilder.Entity<ProfessorSubject>()
                .HasOne(ps => ps.User)
                .WithMany(p => p.ProfessorSubjects)
                .HasForeignKey(ps => ps.UserId);

            // Feedback and User relationship
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ProfessorSubject> ProfessorSubjects { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
