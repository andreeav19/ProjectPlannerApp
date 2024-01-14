using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime? Deadline { get; set; }
        public string? UserId { get; set; } // user assigned to do the task
        public int? StatusId { get; set; }

        public virtual Project Project { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Status Status { get; set; }
        public virtual Feedback Feedback { get; set; }
    }
}
