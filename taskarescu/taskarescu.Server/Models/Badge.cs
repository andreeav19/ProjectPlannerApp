using System.ComponentModel.DataAnnotations;

namespace taskarescu.Server.Models
{
    public class Badge
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<UserBadge> UserBadges { get; set; }
    }
}
