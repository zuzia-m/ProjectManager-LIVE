using ProjectManager.Data.Models;
using ProjectManager.DTOs;

namespace ProjectManager.Services
{
    public interface IProjectService
    {
        public Task<List<ProjectDto>> GetAll();
        public Task<ProjectDto> Create(CreateProjectDto createProjectDto);
    }
}
