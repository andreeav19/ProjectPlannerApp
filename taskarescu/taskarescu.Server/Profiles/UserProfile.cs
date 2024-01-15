using AutoMapper;

namespace taskarescu.Server.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<Models.AppUser, DTOs.UserDto>().ReverseMap();
        }
    }
}
