﻿namespace taskarescu.Server.DTOs
{
    public class ProjectDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }
    }
}
