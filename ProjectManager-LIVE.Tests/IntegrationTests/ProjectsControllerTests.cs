using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using ProjectManager.DTOs;
using ProjectManager.Exceptions;
using ProjectManager.Services;
using System.Net;
using System.Net.Http.Json;

namespace ProjectManager_LIVE.Tests.IntegrationTests
{
    public class ProjectsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IProjectService> _projectServiceMock = new();

        public ProjectsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(ServiceDescriptor.Scoped(typeof(IProjectService),
                        _ => _projectServiceMock.Object));
                });
            });
        }

        [Fact]
        public async Task GetAll_ForValidRequest_ShouldReturn200Ok()
        {
            // arrange
            var client = _factory.CreateClient();
            _projectServiceMock.Setup(service => service.GetAll(null, null, null))
                .ReturnsAsync(new List<ProjectDto>());

            // act
            var response = await client.GetAsync("/api/projects");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ForNonExistingProject_ShouldReturn404NotFound()
        {
            // arrange
            var id = 123;
            var client = _factory.CreateClient();
            _projectServiceMock.Setup(service => service.GetById(id))
                .ThrowsAsync(new NotFoundException($"Project with id {id} does not exist."));

            // act
            var response = await client.GetAsync($"/api/projects/{id}");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForExistingProject_ShouldReturn200Ok()
        {
            // arrange
            var id = 99;
            var name = "Project 1";
            var description = "Project for tests";
            var project = new ProjectDto { Id = id, Name = name, Description = description };
            var client = _factory.CreateClient();
            _projectServiceMock.Setup(service => service.GetById(id))
                .ReturnsAsync(project);

            // act
            var response = await client.GetAsync($"/api/projects/{id}");
            var projectDto = await response.Content.ReadFromJsonAsync<ProjectDto>();

            //assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            projectDto.Should().NotBeNull();
            projectDto.Name.Should().Be(name);
            projectDto.Description.Should().Be(description);
        }

        [Fact]
        public async Task Create_ForValidProject_ShouldReturn200Ok()
        {
            // arrange
            var client = _factory.CreateClient();

            var createProjectDto = new CreateProjectDto
            {
                Name = "New Project",
                Description = "New Description"
            };

            var createdProjectDto = new ProjectDto
            {
                Id = 1,
                Name = createProjectDto.Name,
                Description = createProjectDto.Description
            };

            _projectServiceMock.Setup(p => p.Create(It.IsAny<CreateProjectDto>()))
                .ReturnsAsync(createdProjectDto);

            // act
            var response = await client.PostAsJsonAsync("/api/projects", createProjectDto);
            var projectDto = await response.Content.ReadFromJsonAsync<ProjectDto>();

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            projectDto.Should().NotBeNull();
            projectDto.Name.Should().Be(createProjectDto.Name);
        }

        [Fact]
        public async Task Update_ForValidProject_ShouldReturn200Ok()
        {
            // arrange
            var client = _factory.CreateClient();

            var updateProjectDto = new UpdateProjectDto
            {
                Id = 1,
                Name = "Updated Project",
                Description = "Updated Description"
            };

            var updatedProjectDto = new ProjectDto
            {
                Id = updateProjectDto.Id,
                Name = updateProjectDto.Name,
                Description = updateProjectDto.Description
            };

            _projectServiceMock.Setup(p => p.Update(It.IsAny<UpdateProjectDto>()))
                .ReturnsAsync(updatedProjectDto);

            // act
            var response = await client.PutAsJsonAsync("/api/projects", updateProjectDto);
            var projectDto = await response.Content.ReadFromJsonAsync<ProjectDto>();

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            projectDto.Should().NotBeNull();
            projectDto.Name.Should().Be(updateProjectDto.Name);
        }

        [Fact]
        public async Task Delete_ForExistingId_ShouldReturn204NoContent()
        {
            // arrange
            var id = 1;

            _projectServiceMock.Setup(p => p.Delete(id))
                .Returns(Task.CompletedTask);

            var client = _factory.CreateClient();

            // act
            var response = await client.DeleteAsync($"/api/projects/{id}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
