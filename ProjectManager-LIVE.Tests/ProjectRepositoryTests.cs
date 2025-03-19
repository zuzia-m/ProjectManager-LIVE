using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Data.Models;
using ProjectManager.Exceptions;
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

        [Fact]
        public async Task GetAll_ShouldSortByNameDescending()
        {
            var projects = await _repository.GetAll(null, "name", "desc");
            Assert.Equal(5, projects.Count);
            Assert.Equal("Project E", projects.First().Name);
            Assert.Equal("Project A", projects.Last().Name);
        }

        [Fact]
        public async Task GetAll_ShouldSortByCreatedDateAsc()
        {
            var projects = await _repository.GetAll(null, "createddate", "asc");
            Assert.Equal(5, projects.Count);
            Assert.Equal("Project B", projects.First().Name);
            Assert.Equal("Project D", projects.Last().Name);
        }

        [Fact]
        public async Task GetAll_ShouldSortByCreatedDateDesc()
        {
            var projects = await _repository.GetAll(null, "createddate", "desc");
            Assert.Equal(5, projects.Count);
            Assert.Equal("Project D", projects.First().Name);
            Assert.Equal("Project B", projects.Last().Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnProject()
        {
            var project = await _repository.GetById(1);
            Assert.NotNull(project);
            Assert.Equal("Project D", project.Name);
        }

        [Fact]
        public async Task GetById_ShouldThrowNotFoundException_WhenProjectDoesNotExist()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.GetById(99));
        }

        [Fact]
        public async Task Create_ShouldAddProject()
        {
            Assert.Equal(5, _context.Projects.Count());

            var newProject = new Project { Name = "Project F", Description = "Description F" };
            await _repository.Create(newProject);

            Assert.Equal(6, _context.Projects.Count());
        }

        [Fact]
        public async Task Update_ShouldModifyProject()
        {
            var project = await _repository.GetById(1);
            project.Name = "Updated Project D";
            await _repository.Update(project);

            var updatedProject = await _repository.GetById(1);
            Assert.Equal(project.Name, updatedProject.Name);
        }

        [Fact]
        public async Task Delete_ShouldRemoveProject()
        {
            Assert.Equal(5, _context.Projects.Count());
            await _repository.Delete(1);
            Assert.Equal(4, _context.Projects.Count());
        }

        [Fact]
        public async Task Delete_ShouldThrowNotFoundException_WhenProjectDoesNotExist()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.Delete(8));
        }

        private void SeedDatabase()
        {
            _context.Projects.AddRange(new List<Project>
            {
                 new Project { Name = "Project D", Description = "Description D", CreatedDate = new DateTime(2023, 1, 30) },
                 new Project { Name = "Project A", Description = "Description A", CreatedDate = new DateTime(2023, 1, 3) },
                 new Project { Name = "Project C", Description = "Description C", CreatedDate = new DateTime(2023, 1, 8) },
                 new Project { Name = "Project E", Description = "Description E", CreatedDate = new DateTime(2023, 1, 15) },
                 new Project { Name = "Project B", Description = "Description B", CreatedDate = new DateTime(2023, 1, 1) }
            });

            _context.SaveChanges();
        }
    }
}
