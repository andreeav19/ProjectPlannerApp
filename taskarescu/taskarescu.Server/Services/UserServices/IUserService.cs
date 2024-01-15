using taskarescu.Server.DTOs;
using FluentResults;

namespace taskarescu.Server.Services.UserServices
{
    public interface IUserService
    {
        Task<ResultDto<ICollection<UserDto>>> GetUsers();
        Task<ResultDto<UserDto>> GetUserById(string userId);
        Task<Result<string>> EditUserRole(string userId, string newRole);
        Task<ResultDto<ICollection<BadgeDto>>> GetBadgesByUserId(string userId);
        //Task<ResultDto<ICollection<UserScoreDto>>> GetLeaderBoardUsers();
    }
}
