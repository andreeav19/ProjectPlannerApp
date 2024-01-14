using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace taskarescu.Server.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserBadge> UserBadges { get; set; }
        public virtual ICollection<StudentProject> StudentProjects { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }

        [JsonIgnore]
        public virtual ICollection<Feedback> Feedback { get; set; }

    }
}
