﻿using ProjectManager.DTOs;

namespace ProjectManager.Services
{
    public interface IProjectService
    {
        public Task<List<ProjectDto>> GetAll(string? searchText, string? sortBy, string? sortDirection);

        public Task<ProjectDto> GetById(int id);

        public Task<ProjectDto> Create(CreateProjectDto createProjectDto);

        public Task<ProjectDto> Update(UpdateProjectDto updateProjectDto);

        public Task Delete(int id);
    }
}