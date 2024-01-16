using AutoMapper;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Db;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;

namespace taskarescu.Server.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Models.AppUser, DTOs.UserDto>().ReverseMap();
        }
    }
}
