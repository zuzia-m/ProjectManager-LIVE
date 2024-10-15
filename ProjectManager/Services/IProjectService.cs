using ProjectManager.Data.Models;

namespace ProjectManager.Services
{
    public interface IProjectService
    {
        public Task<List<Project>> GetAll();
    }
}
