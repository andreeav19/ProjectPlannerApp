namespace taskarescu.Server.DTOs
{
    public class PutPostTaskItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Username { get; set; }
        public string StatusName{ get; set; }
    }
}
