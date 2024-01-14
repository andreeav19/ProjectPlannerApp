using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }    // professor who created the project

        public virtual AppUser User { get; set; }
        public virtual ICollection<StudentProject> StudentProjects { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
