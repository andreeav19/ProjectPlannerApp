using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Stage
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Points { get; set; }
        public Guid ProjectId { get; set; }
        public int? StatusId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
