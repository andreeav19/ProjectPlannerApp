using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.BadgeServices
{
    public interface IBadgeService
    {
        Task<ResultDto<ICollection<BadgeDto>>> GetBadges();
        Task<ResultDto<BadgeDto>> GetBadgeById(int badgeId);
        Task<ResultDto<int>> AddBadge(BadgeDto badgeDto);
        Task<ResultDto<bool>> DeleteBadgeById(int badgeId);
        Task<ResultDto<bool>> EditBadgeById(int badgeId);
        Task<ResultDto<bool>> AddBadgeToUser(int badgeId, string userId);
    }
}
