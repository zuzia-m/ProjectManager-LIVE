using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProjectTask>> GetAll()
        {
            return await _context.ProjectTasks
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
