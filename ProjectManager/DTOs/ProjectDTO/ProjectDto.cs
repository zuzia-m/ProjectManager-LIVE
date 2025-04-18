﻿using ProjectManager.DTOs.ProjectTaskDTO;

namespace ProjectManager.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public List<ProjectTaskDto> ProjectTasks { get; set; }
    }
}