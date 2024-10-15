using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Models;
using ProjectManager.DTOs;
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

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto createProjectDto)
        {
            var projectDto = await _projectService.Create(createProjectDto);
            return Ok(projectDto);
        }
    }
}
