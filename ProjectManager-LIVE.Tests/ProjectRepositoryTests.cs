using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Data.Models;
using ProjectManager.Repositories;

namespace ProjectManager_LIVE.Tests
{
    public class ProjectRepositoryTests
    {
        private readonly ProjectManagerDbContext _context;
        private readonly ProjectRepository _repository;

        public ProjectRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProjectManagerDbContext>()
                 //.UseInMemoryDatabase(databaseName: "TestDb")
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ProjectManagerDbContext(options);

            SeedDatabase();

            _repository = new ProjectRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProjects()
        {
            var projects = await _repository.GetAll(null, null, null);
            Assert.Equal(5, projects.Count);
        }

        [Fact]
        public async Task GetAll_ShouldFilterBySearchText()
        {
            var projects = await _repository.GetAll("A", null, null);
            Assert.Single(projects);
            Assert.Equal("Project A", projects.First().Name);

            projects = await _repository.GetAll("Description C", null, null);
            Assert.Single(projects);
            Assert.Equal("Project C", projects.First().Name);
        }

        [Fact]
        public async Task GetAll_ShouldSortByNameAscending()
        {
            var projects = await _repository.GetAll(null, "name", "asc");
            Assert.Equal(5, projects.Count);
            Assert.Equal("Project A", projects.First().Name);
            Assert.Equal("Project E", projects.Last().Name);
        }

        private void SeedDatabase()
        {
            _context.Projects.AddRange(new List<Project>
            {
                 new Project { Name = "Project D", Description = "Description D", CreatedDate = new DateTime(2023, 1, 13) },
                 new Project { Name = "Project A", Description = "Description A", CreatedDate = new DateTime(2023, 1, 30) },
                 new Project { Name = "Project C", Description = "Description C", CreatedDate = new DateTime(2023, 1, 8) },
                 new Project { Name = "Project E", Description = "Description E", CreatedDate = new DateTime(2023, 1, 1) },
                 new Project { Name = "Project B", Description = "Description B", CreatedDate = new DateTime(2023, 1, 15) }
            });

            _context.SaveChanges();
        }
    }
}
