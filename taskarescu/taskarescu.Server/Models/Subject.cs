using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace taskarescu.Server.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProfessorSubject> ProfessorSubjects { get; set; }

        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

    }
}
