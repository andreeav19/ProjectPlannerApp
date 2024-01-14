using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public Guid UserId { get; set; }
        public int TaskItemId { get; set; }
        public int DifficultyId { get; set; }

        public virtual User User { get; set; }
        public virtual TaskItem TaskItem { get; set; }
        public virtual Difficulty Difficulty { get; set; }
    }
}
