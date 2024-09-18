using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Models;

namespace ProjectManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public static List<Project> projects = new List<Project>
        {
            new Project
            {
                 Id = 1,
                 Name = "Nauka Programowania w C#",
                 Description = "Projekt mający na celu naukę podstaw programowania w języku C#."
            },
            new Project
            {
                Id = 2,
                Name = "Tworzenie i Zarządzanie Bazami Danych SQL",
                Description = "Nauka baz danych SQL"
            },
            new Project
            {
                 Id = 3,
                 Name = "Wprowadzenie do ASP.NET Core",
                 Description = "Projekt mający na celu naukę tworzenia aplikacji webowych przy użyciu frameworka ASP.NET Core."
            }
        };

        [HttpGet]
        public ActionResult<List<Project>> GetAll()
        {
            return Ok(projects);
        }

        [HttpPost]
        public ActionResult Add(Project project)
        {
            projects.Add(project);
            return Ok();
        }
    }
}
