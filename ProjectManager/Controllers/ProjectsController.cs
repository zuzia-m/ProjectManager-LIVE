using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Models;
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
        public async Task<ActionResult<List<Project>>> GetAll()
        {
            var projects = await _projectService.GetAll();
            return Ok(projects);
        }
    }
}
