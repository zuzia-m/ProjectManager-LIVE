using AutoMapper;
using ProjectManager.Data.Models;
using ProjectManager.DTOs.ProjectTaskDTO;
using ProjectManager.Repositories;

namespace ProjectManager.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;
        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectTaskDto>> GetAll()
        {
            var projectTasks = await _projectTaskRepository.GetAll();
            return _mapper.Map<List<ProjectTaskDto>>(projectTasks);
        }

        public async Task<List<ProjectTaskDto>> GetByProjectId(int projectId)
        {
            var projectTasks = await _projectTaskRepository.GetByProjectId(projectId);
            return _mapper.Map<List<ProjectTaskDto>>(projectTasks);
        }

        public async Task<ProjectTaskDto?> GetById(int id)
        {
            var projectTask = await _projectTaskRepository.GetById(id);
            return _mapper.Map<ProjectTaskDto>(projectTask);
        }

        public async Task<ProjectTaskDto> Add(CreateProjectTaskDto createProjectTaskDto)
        {
            var projectTaskToAdd = _mapper.Map<ProjectTask>(createProjectTaskDto);
            var projectTask = await _projectTaskRepository.Add(projectTaskToAdd);
            return _mapper.Map<ProjectTaskDto>(projectTask);
        }

        public async Task<ProjectTaskDto> Update(UpdateProjectTaskDto updateProjectTaskDto)
        {
            var projectTask = _mapper.Map<ProjectTask>(updateProjectTaskDto);
            var updatedProjectTask = await _projectTaskRepository.Update(projectTask);
            return _mapper.Map<ProjectTaskDto>(updatedProjectTask);
        }

        public Task Delete(int id)
        {
            return _projectTaskRepository.Delete(id);
        }
    }
}
