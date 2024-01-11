namespace taskarescu.Server.Models
{
    public class ProfessorSubject
    {
        public Guid UserId { get; set; }
        public int SubjectId { get; set; }

        public virtual User User { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
