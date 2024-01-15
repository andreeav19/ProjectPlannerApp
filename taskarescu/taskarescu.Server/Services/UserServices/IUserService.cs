using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.UserServices
{
    public interface IUserService
    {
        Task<ResultDto<ICollection<UserDto>>> GetUsers();
        Task<ResultDto<UserDto>> GetUserById(string userId);
    }
}
