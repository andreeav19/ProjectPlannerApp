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
        public Guid? UserId { get; set; } // user assigned to do the task
        public int? DifficultyId { get; set; }
        public int? StatusId { get; set; }
        public int? Points { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        public virtual Difficulty Difficulty { get; set; }
        public virtual Status Status { get; set; }
    }
}
