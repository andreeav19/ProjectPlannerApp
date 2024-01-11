using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
