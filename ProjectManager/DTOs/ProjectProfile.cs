using AutoMapper;
using ProjectManager.Data.Models;

namespace ProjectManager.DTOs
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd MMMM yyyy")));

            CreateMap<CreateProjectDto, Project>();        

            CreateMap<UpdateProjectDto, Project>();
        }
    }
}
