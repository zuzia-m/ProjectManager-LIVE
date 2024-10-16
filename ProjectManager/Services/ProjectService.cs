﻿using AutoMapper;
using ProjectManager.Data.Models;
using ProjectManager.DTOs;
using ProjectManager.Repositories;

namespace ProjectManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Create(CreateProjectDto createProjectDto)
        {
            var project = _mapper.Map<Project>(createProjectDto);
            project = await _projectRepository.Create(project);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<List<ProjectDto>> GetAll()
        {
            var projects = await _projectRepository.GetAll();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            return projectDtos;
        }
    }
}
