﻿using Microsoft.AspNetCore.Mvc;
using ProjectManager.DTOs.ProjectTaskDTO;

namespace ProjectManager.Services
{
    public interface IProjectTaskService
    {
        public Task<List<ProjectTaskDto>> GetAll(string? searchText, DateTime? dueDate, bool? isCompleted, string? sortBy, string? sortDirection);
        public Task<ProjectTaskDto> GetById(int id);
        public Task<List<ProjectTaskDto>> GetByProjectId(int projectId);
        public Task<ProjectTaskDto> Add(CreateProjectTaskDto createProjectTaskDto);
        public Task<ProjectTaskDto> Update(UpdateProjectTaskDto updateProjectTaskDto);
        public Task Delete(int id);
    }
}
