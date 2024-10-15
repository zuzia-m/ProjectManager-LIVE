using ProjectManager.Data.Models;

namespace ProjectManager.Repositories
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetAll();
    }
}
