using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }

    }
}
