using Microsoft.AspNetCore.Mvc;
using ProjectManager.DTOs;
using ProjectManager.Exceptions;
using ProjectManager.Services;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
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
            try
            {
                var project = await _projectService.GetById(id);
                return Ok(project);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] CreateProjectDto createProjectDto)
        {
            var projectDto = await _projectService.Create(createProjectDto);
            return Ok(projectDto);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDto>> Update([FromBody] UpdateProjectDto updateProjectDto)
        {
            try
            {
                var projectDto = await _projectService.Update(updateProjectDto);
                return Ok(projectDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
