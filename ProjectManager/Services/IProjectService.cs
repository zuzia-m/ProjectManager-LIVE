using ProjectManager.Data.Models;
using ProjectManager.DTOs;
using System.Diagnostics.Contracts;

namespace ProjectManager.Services
{
    public interface IProjectService
    {
        public Task<List<ProjectDto>> GetAll();
        public Task<ProjectDto> GetById(int id);
        public Task<ProjectDto> Create(CreateProjectDto createProjectDto);
        public Task<ProjectDto> Update(UpdateProjectDto updateProjectDto);
    }
}
