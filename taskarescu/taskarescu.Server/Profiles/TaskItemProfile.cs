using AutoMapper;

namespace taskarescu.Server.Profiles
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile() {
            CreateMap<Models.TaskItem, DTOs.TaskItemDto>().ReverseMap();
        }
    }
}
