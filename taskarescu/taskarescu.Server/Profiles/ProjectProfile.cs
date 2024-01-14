using AutoMapper;

namespace taskarescu.Server.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile() { 
            // source and destination
            CreateMap<Models.Project, DTOs.ProjectDto>().ReverseMap();
        }
    }
}
