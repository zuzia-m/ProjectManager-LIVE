using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DTOs;
using ProjectManager.Services;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IValidator<CreateProjectDto> _createProjectDtoValidator;
        private readonly IValidator<UpdateProjectDto> _updateProjectDtoValidator;

        public ProjectsController(
            IProjectService projectService,
            IValidator<CreateProjectDto> createProjectDtoValidator,
            IValidator<UpdateProjectDto> updateProjectDtoValidator)
        {
            _projectService = projectService;
            _createProjectDtoValidator = createProjectDtoValidator;
            _updateProjectDtoValidator = updateProjectDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAll()
        {
            var projectDtos = await _projectService.GetAll();
            return Ok(projectDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById([FromRoute] int id)
        {
            var project = await _projectService.GetById(id);
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] CreateProjectDto createProjectDto)
        {
            _createProjectDtoValidator.ValidateAndThrow(createProjectDto);
            var projectDto = await _projectService.Create(createProjectDto);
            return Ok(projectDto);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDto>> Update([FromBody] UpdateProjectDto updateProjectDto)
        {
            _updateProjectDtoValidator.ValidateAndThrow(updateProjectDto);
            var projectDto = await _projectService.Update(updateProjectDto);
            return Ok(projectDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _projectService.Delete(id);
            return NoContent();
        }
    }
}