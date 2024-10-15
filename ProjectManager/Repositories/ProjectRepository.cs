﻿using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Data.Models;

namespace ProjectManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectManagerDbContext _context;
        public ProjectRepository(ProjectManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAll()
        {
            var projects = await _context.Projects.ToListAsync();
            return projects;
        }
    }
}