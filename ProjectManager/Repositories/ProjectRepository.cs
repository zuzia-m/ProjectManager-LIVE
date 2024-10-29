using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Data.Models;
using ProjectManager.Exceptions;

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

        public async Task<Project> GetById(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project is null)
            {
                throw new NotFoundException($"Project with id {id} does not exist.");
            }

            return project;
        }

        public async Task<Project> Create(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> Update(Project project)
        {
            var existingProject = await _context.Projects.FindAsync(project.Id);

            if (existingProject is null)
            {
                throw new NotFoundException($"Project with id {project.Id} does not exist.");
            }

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            await _context.SaveChangesAsync();

            return existingProject;
        }
    }
}
