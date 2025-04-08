using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using ProjectManager.DTOs;
using ProjectManager.Exceptions;
using ProjectManager.Services;
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
    }
}
