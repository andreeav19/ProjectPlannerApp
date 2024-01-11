using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Difficulty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
