using ProjectManager.Data.Models;

namespace ProjectManager.Repositories
{
    public interface IProjectTaskRepository
    {
        public Task<List<ProjectTask>> GetAll(string? searchText, DateTime? dueDate, bool? isCompleted);
        public Task<ProjectTask?> GetById(int id);
        public Task<List<ProjectTask>> GetByProjectId(int projectId);
        public Task<ProjectTask> Add(ProjectTask projectTask);
        public Task<ProjectTask> Update(ProjectTask projectTask);
        public Task Delete(int id);
    }
}
