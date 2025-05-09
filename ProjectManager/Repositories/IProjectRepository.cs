﻿using ProjectManager.Data.Models;

namespace ProjectManager.Repositories
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetAll(string? searchText, string? sortBy, string? sortDirection);

        public Task<Project> GetById(int id);

        public Task<Project> Create(Project project);

        public Task<Project> Update(Project project);

        public Task Delete(int id);
    }
}