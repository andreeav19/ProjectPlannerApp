namespace taskarescu.Server.Models
{
    public class StudentProject
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }

        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }
}
