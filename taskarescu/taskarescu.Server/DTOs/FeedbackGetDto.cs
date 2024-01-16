namespace taskarescu.Server.DTOs
{
    public class FeedbackGetDto
    {
        public string Description { get; set; }
        public int Points { get; set; }
        public string UserId { get; set; }
        public int TaskItemId { get; set; }
        public int DifficultyId { get; set; }
    }
}
