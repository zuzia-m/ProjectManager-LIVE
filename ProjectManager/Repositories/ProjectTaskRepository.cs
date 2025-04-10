﻿using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Data.Models;
using ProjectManager.Exceptions;

namespace ProjectManager.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ProjectManagerDbContext _context;
        public ProjectTaskRepository(ProjectManagerDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectTask> Add(ProjectTask projectTask)
        {
            await _context.ProjectTasks.AddAsync(projectTask);
            await _context.SaveChangesAsync();
            return projectTask;
        }

        public async Task Delete(int id)
        {
            var existingProjectTask = await _context.ProjectTasks.FindAsync(id);
            if (existingProjectTask is null)
            {
                throw new NotFoundException($"ProjectTask with id {id} not found.");
            }
            _context.ProjectTasks.Remove(existingProjectTask);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProjectTask>> GetAll(string? searchText, DateTime? dueDate, bool? isCompleted, string? sortBy, string? sortDirection)
        {
            var query = _context.ProjectTasks.AsQueryable();

            // Wyszukiwanie po tytule lub opisie
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(pt => pt.Title.Contains(searchText.ToLower()) || pt.Description.Contains(searchText.ToLower()));
            }

            // Filtrowanie po dacie
            if (dueDate.HasValue)
            {
                query = query.Where(pt => pt.DueDate <= dueDate);
            }

            // Filtrowanie po statusie ukończenia
            if (isCompleted.HasValue)
            {
                query = query.Where(pt => pt.IsCompleted == isCompleted);
            }

            if (!string.IsNullOrEmpty(sortDirection))
            {
                // sortDirection = string.IsNullOrEmpty(sortDirection) ? "asc" : sortDirection; 
                sortBy = string.IsNullOrEmpty(sortBy) ? "" : sortBy;

                switch (sortBy.ToLower())
                {
                    case "title":
                        query = sortDirection.ToLower() == "asc" ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title);
                        break;
                    case "duedate":
                        query = sortDirection.ToLower() == "asc" ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate);
                        break;
                    case "iscompleted":
                        query = sortDirection.ToLower() == "asc" ? query.OrderBy(t => t.IsCompleted) : query.OrderByDescending(t => t.IsCompleted);
                        break;
                    default:
                        query = sortDirection.ToLower() == "asc" ? query.OrderBy(t => t.Id) : query.OrderByDescending(t => t.Id);
                        break;
                }
            }

            return await query
            .Include(pt => pt.Project)
            .ToListAsync();
        }

        public async Task<ProjectTask?> GetById(int id)
        {
            var projectTask = await _context.ProjectTasks
                .Include(pt => pt.Project)
                .FirstOrDefaultAsync(pt => pt.Id == id);

            if (projectTask is null)
            {
                throw new NotFoundException($"ProjectTask with id {id} not found.");
            }

            return projectTask;
        }

        public async Task<List<ProjectTask>> GetByProjectId(int projectId)
        {
            var projectExists = await _context.Projects
                .AnyAsync(p => p.Id == projectId);

            if (!projectExists)
            {
                throw new NotFoundException($"Project with id {projectId} not found.");
            }

            return await _context.ProjectTasks
                .Where(pt => pt.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<ProjectTask> Update(ProjectTask projectTask)
        {
            var existingProjectTask = await _context.ProjectTasks.FindAsync(projectTask.Id);

            if (existingProjectTask is null)
            {
                throw new NotFoundException($"ProjectTask with id {projectTask.Id} not found.");
            }

            existingProjectTask.Title = projectTask.Title;
            existingProjectTask.Description = projectTask.Description;
            existingProjectTask.DueDate = projectTask.DueDate;
            existingProjectTask.IsCompleted = projectTask.IsCompleted;

            await _context.SaveChangesAsync();
            return existingProjectTask;
        }
    }
}
