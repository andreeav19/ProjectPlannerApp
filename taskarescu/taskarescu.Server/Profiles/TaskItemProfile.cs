using AutoMapper;

namespace taskarescu.Server.Profiles
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile() {
            CreateMap<Models.TaskItem, DTOs.TaskItemDto>().ReverseMap();

            CreateMap<Models.TaskItem, DTOs.GetTaskItemDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ReverseMap();
        }
    }
}
