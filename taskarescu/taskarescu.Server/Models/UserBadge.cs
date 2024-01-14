namespace taskarescu.Server.Models
{
    public class UserBadge
    {
        public string UserId { get; set; }
        public int BadgeId { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
