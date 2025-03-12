using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectManager.DTOs;
using ProjectManager.DTOs.ProjectTaskDTO;
using ProjectManager.Repositories;
using ProjectManager.Services;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly IValidator<CreateProjectTaskDto> _createProjectTaskDtoValidator;
        private readonly IValidator<UpdateProjectTaskDto> _updateProjectTaskDtoValidator;

        public ProjectTasksController(IProjectTaskService projectTaskService,
                    IValidator<CreateProjectTaskDto> createProjectTaskDtoValidator,
                    IValidator<UpdateProjectTaskDto> updateProjectTaskDtoValidator)
        {
            _projectTaskService = projectTaskService;
            _createProjectTaskDtoValidator = createProjectTaskDtoValidator;
            _updateProjectTaskDtoValidator = updateProjectTaskDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectTaskDto>>> GetAll(
            [FromQuery] string? searchText,
            [FromQuery] DateTime? dueDate,
            [FromQuery] bool? isCompleted,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection
            )
        {
            var projectTasks = await _projectTaskService.GetAll(searchText, dueDate, isCompleted, sortBy, sortDirection);
            return Ok(projectTasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTaskDto>> GetById([FromRoute] int id)
        {
            var projectTask = await _projectTaskService.GetById(id);
            return Ok(projectTask);
        }

        [HttpGet("byProject/{projectId}")]
        public async Task<ActionResult<List<ProjectTaskDto>>> GetByProjectId([FromRoute] int projectId)
        {
            var projectTasks = await _projectTaskService.GetByProjectId(projectId);
            return Ok(projectTasks);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTaskDto>> Add([FromBody] CreateProjectTaskDto createProjectTaskDto)
        {
            _createProjectTaskDtoValidator.ValidateAndThrow(createProjectTaskDto);
            var projectTask = await _projectTaskService.Add(createProjectTaskDto);
            return Ok(projectTask);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectTaskDto>> Update([FromBody] UpdateProjectTaskDto updateProjectTaskDto)
        {
            _updateProjectTaskDtoValidator.ValidateAndThrow(updateProjectTaskDto);
            var projectTask = await _projectTaskService.Update(updateProjectTaskDto);
            return Ok(projectTask);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _projectTaskService.Delete(id);
            return NoContent();
        }
    }
}
