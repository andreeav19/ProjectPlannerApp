using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace taskarescu.Server.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? SubjectId { get; set; }
        public Guid UserId { get; set; }    // user who created the project
        public int? StatusId { get; set; }

        public virtual Subject Subject {  get; set; }
        public virtual User User { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
