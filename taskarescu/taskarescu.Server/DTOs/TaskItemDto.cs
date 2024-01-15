namespace taskarescu.Server.DTOs
{
    public class TaskItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string? UserId { get; set; }
        public int? StatusId { get; set; }
    }
}
