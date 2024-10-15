using ProjectManager.Data.Models;
using ProjectManager.Repositories;

namespace ProjectManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetAll()
        {
            return await _projectRepository.GetAll();
        }
    }
}
