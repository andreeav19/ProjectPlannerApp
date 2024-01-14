using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace taskarescu.Server.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser() : base() { }
        // pe langa multe alte chestii astea sunt deja in IdentityUser si pare ca e nevoie sa nu le definim si noi
        //[Key]
        //public Guid Id { get; set; }
        //public string Email { get; set; }
        //public string Username { get; set; }
        // public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleId { get; set; }

        //public virtual AppRole Role { get; set; }
        public virtual ICollection<UserBadge> UserBadges { get; set; }
        public virtual ICollection<StudentProject> StudentProjects { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }

        [JsonIgnore]
        public virtual ICollection<Feedback> Feedback { get; set; }

    }
}
