using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.BadgeServices
{
    public class BadgeService : IBadgeService
    {
        public Task<ResultDto<int>> AddBadge(BadgeDto badgeDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto<bool>> AddBadgeToUser(int badgeId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto<bool>> DeleteBadgeById(int badgeId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto<bool>> EditBadgeById(int badgeId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto<BadgeDto>> GetBadgeById(int badgeId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto<ICollection<BadgeDto>>> GetBadges()
        {
            throw new NotImplementedException();
        }
    }
}
