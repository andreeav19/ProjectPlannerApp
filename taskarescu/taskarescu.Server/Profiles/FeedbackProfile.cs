using AutoMapper;

namespace taskarescu.Server.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile() {
            CreateMap<Models.Feedback, DTOs.FeedbackGetDto>().ReverseMap();
            CreateMap<Models.Feedback, DTOs.FeedbackDto>().ReverseMap();
        }
    }
}
