using AutoMapper;

namespace taskarescu.Server.Profiles
{
    public class BadgeProfile : Profile
    {
        public BadgeProfile()
        {
            CreateMap<Models.Badge, DTOs.BadgeDto>().ReverseMap();
        }
    }
}
