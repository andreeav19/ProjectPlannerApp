namespace taskarescu.Server.Models
{
    public class StudentProject
    {
        public string UserId { get; set; }
        public Guid ProjectId { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Project Project { get; set; }
    }
}
