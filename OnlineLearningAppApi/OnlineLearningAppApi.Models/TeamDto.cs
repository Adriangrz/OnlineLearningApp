﻿namespace OnlineLearningAppApi.Models
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string? ImagePath { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
        public bool IsArchived { get; set; }
    }
}
