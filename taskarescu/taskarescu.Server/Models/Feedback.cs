using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int StageId { get; set; }
        public Guid UserId { get; set; }

        public virtual Stage Stage { get; set; }
        public virtual User User { get; set; }
    }
}
