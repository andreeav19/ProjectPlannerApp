using taskarescu.Server.Models;

namespace taskarescu.Server.DTOs
{
    public class TaskItemDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string? UserId { get; set; }
        public int? StatusId { get; set; }

        public Feedback? Feedback { get; set; }
    }
}
