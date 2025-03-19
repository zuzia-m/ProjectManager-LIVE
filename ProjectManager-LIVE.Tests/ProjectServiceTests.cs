using AutoMapper;
using Moq;
using ProjectManager.Data.Models;
using ProjectManager.DTOs;
using ProjectManager.Repositories;
using ProjectManager.Services;

namespace ProjectManager_LIVE.Tests
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProjectService _projectService;
        public ProjectServiceTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _mapperMock = new Mock<IMapper>();
            _projectService = new ProjectService(_projectRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnProjectDtos()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project { Id = 1, Name = "Project 1" },
                new Project { Id = 2, Name = "Project 2" }
            };

            _projectRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(projects);

            var projectDtos = new List<ProjectDto>
            {
                new ProjectDto { Id = 1, Name = "Project 1" },
                new ProjectDto { Id = 2, Name = "Project 2" }
            };

            _mapperMock.Setup(mapper => mapper.Map<List<ProjectDto>>(projects))
                .Returns(projectDtos);

            // Act
            var result = await _projectService.GetAll(null, null, null);

            // Assert
            _projectRepositoryMock.Verify(repo => repo.GetAll(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Project 1", result[0].Name);
            Assert.Equal("Project 2", result[1].Name);
        }

        [Fact]
        public async Task Add_ShouldReturnProjectDto()
        {
            // Assert
            var createProjectDto = new CreateProjectDto { Name = "New Project" };
            var project = new Project { Id = 1, Name = "New Project" };

            _mapperMock.Setup(mapper => mapper.Map<Project>(createProjectDto))
                .Returns(project);

            _projectRepositoryMock.Setup(repo => repo.Create(project))
                .ReturnsAsync(project);

            var projectDto = new ProjectDto { Id = 1, Name = "New Project" };
            _mapperMock.Setup(mapper => mapper.Map<ProjectDto>(project))
                .Returns(projectDto);

            // Act
            var result = await _projectService.Create(createProjectDto);

            // Assert
            _projectRepositoryMock.Verify(repo => repo.Create(project), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("New Project", result.Name);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedProjectDto()
        {
            // Arrange
            var updateProjectDto = new UpdateProjectDto { Id = 2, Name = "Updated Project" };
            var project = new Project { Id = 2, Name = "Updated Project" };

            _mapperMock.Setup(mapper => mapper.Map<Project>(updateProjectDto))
                .Returns(project);

            _projectRepositoryMock.Setup(repo => repo.Update(project))
                .ReturnsAsync(project);

            var projectDto = new ProjectDto { Id = 2, Name = "Updated Project" };
            _mapperMock.Setup(mapper => mapper.Map<ProjectDto>(project))
                .Returns(projectDto);

            // Act
            var result = await _projectService.Update(updateProjectDto);

            // Assert
            _projectRepositoryMock.Verify(repo => repo.Update(project), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Updated Project", result.Name);
        }

        [Fact]
        public async Task Delete_ShouldCallDeleteOnRepository()
        {
            // Arrange
            _projectRepositoryMock.Setup(repo => repo.Delete(1))
                .Returns(Task.CompletedTask);

            // Act
            await _projectService.Delete(1);

            // Assert
            _projectRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
