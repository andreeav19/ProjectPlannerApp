namespace taskarescu.Server.Models
{
    public class UserBadge
    {
        public Guid UserId { get; set; }
        public int BadgeId { get; set; }

        public virtual User User { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
