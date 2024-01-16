using AutoMapper;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;

namespace taskarescu.Server.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<Models.AppUser, DTOs.UserDto>().ReverseMap();
            CreateMap<Models.AppUser, DTOs.UserScoreDto>()
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => CalculateTotalPoints(src)))
            .ReverseMap();
        }

        private int CalculateTotalPoints(Models.AppUser user)
        {
            return user.Feedback
                .Where(f => user.TaskItems.Any(t => t.Id == f.TaskItemId))
                .Sum(f => f.Points);
        }
    }
    
}
