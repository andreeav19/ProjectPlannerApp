using AutoMapper;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Db;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;

namespace taskarescu.Server.Services.BadgeServices
{
    public class BadgeService : IBadgeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BadgeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> AddBadge(BadgeDto badgeDto)
        {
            var badge = new Badge
            {
                Name = badgeDto.Name,
                Description = badgeDto.Description,
            };

            _context.Badges.Add(badge);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<int>(false, -1, new[] { "Eroare la adaugarea insigner!" });
            }

            return new ResultDto<int>(true, badge.Id, null);
        }

        public async Task<ResultDto<bool>> AddBadgeToUser(int badgeId, string userId)
        {
            var badge = await _context.Badges.FirstOrDefaultAsync(b => b.Id == badgeId);

            if (badge == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Insigna nu a fost gasita!" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Utilizatorul nu a fost gasit" });
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (role.Name != "Student")
            {
                return new ResultDto<bool>(false, false, new[] { "Nu se poate adauga o insigna unui utilizator care nu este student" });
            }

            var userBadge = await _context.UserBadges.FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BadgeId == badgeId);

            if (userBadge != null) 
            {
                return new ResultDto<bool>(false, false, new[] { "Utilizatorul are deja insigna de acest tip!" });
            }

            var newUserBadge = new UserBadge
            {
                BadgeId = badgeId,
                UserId = userId
            };

            await _context.UserBadges.AddAsync(newUserBadge);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<bool>(false, false, new[] { "Eroare la adaugarea insignei unui utilizator!" });
            }

            return new ResultDto<bool>(true, true, null);
        }

        public async Task<ResultDto<bool>> DeleteBadgeById(int badgeId)
        {
            var badge = await _context.Badges.FirstOrDefaultAsync(b => b.Id ==  badgeId);

            if (badge == null) {
                return new ResultDto<bool>(false, false, new[] { "Insigna nu a fost gasita!" });
            }

            _context.Badges.Remove(badge);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<bool>(false, false, new[] { "Eroare la stergerea insignei!" });
            }

            return new ResultDto<bool>(true, true, null);
        }

        public async Task<ResultDto<bool>> EditBadgeById(int badgeId, BadgeDto badgeDto)
        {
            var badge = await _context.Badges.FirstOrDefaultAsync(b =>b.Id == badgeId);

            if (badge == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Insigna nu a fost gasita!" });
            }

            badge.Name = badgeDto.Name;
            badge.Description = badgeDto.Description;

            _context.Badges.Update(badge);
            await _context.SaveChangesAsync();

            return new ResultDto<bool>(true, true, null);
        }

        public async Task<ResultDto<BadgeDto>> GetBadgeById(int badgeId)
        {
            var badge = await _context.Badges.FirstOrDefaultAsync(b => b.Id == badgeId);

            if (badge == null)
            {
                return new ResultDto<BadgeDto>(false, null, new[] { "Insigna nu a fost gasita!" });
            }

            var badgeDto = _mapper.Map<BadgeDto>(badge);
            return new ResultDto<BadgeDto>(true, badgeDto, null);
        }

        public async Task<ResultDto<ICollection<BadgeDto>>> GetBadges()
        {
            var badges = await _context.Badges.ToListAsync();
            var badgeDtos = _mapper.Map<ICollection<BadgeDto>>(badges);

            return new ResultDto<ICollection<BadgeDto>>(true, badgeDtos, null);
        }
    }
}
