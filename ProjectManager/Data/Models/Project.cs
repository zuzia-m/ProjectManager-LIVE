﻿namespace ProjectManager.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
    }
}