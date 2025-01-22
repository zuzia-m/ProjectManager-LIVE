using AutoMapper;
using ProjectManager.Data.Models;

namespace ProjectManager.DTOs.ProjectTaskDTO
{
    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<ProjectTask, ProjectTaskDto>()
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.ToString("dd MMMM yyyy")));

            CreateMap<CreateProjectTaskDto, ProjectTask>();

            CreateMap<UpdateProjectTaskDto, ProjectTask>();
        }
    }
}
