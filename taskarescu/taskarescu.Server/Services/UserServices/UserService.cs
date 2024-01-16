using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Db;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;

namespace taskarescu.Server.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager) {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result<string>> EditUserRole(string userId, string newRole)
        {
            var newRoles = new List<string> { newRole };
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Result.Fail(new Error("Utilizatorul nu a fost gasit!"));
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == newRole);

            if (role == null)
            {
                return Result.Fail(new Error("Rolul nu a fost gasit!"));
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = newRoles.Except(userRoles);
            var rolesToRemove = userRoles.Except(newRoles);

            var addToRolesResult = await _userManager.AddToRolesAsync(user, rolesToAdd);

            if (!addToRolesResult.Succeeded)
            {
                return Result.Fail(new Error("Eroare la adaugarea rolului nou."));
            }

            var removeFromRolesResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (!removeFromRolesResult.Succeeded)
            {
                return Result.Fail(new Error("Eroare la stergerea rolului vechi."));
            }

            user.RoleId = role.Id;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Result.Ok("Rol schimbat cu succes.");
        }

        public async Task<ResultDto<ICollection<BadgeDto>>> GetBadgesByUserId(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<ICollection<BadgeDto>>(false, null, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (userRole == null)
            {
                return new ResultDto<ICollection<BadgeDto>>(false, null, new[] { "Rolul utilizatorului nu a fost gasit!" });
            }

            if (userRole.Name != "Student")
            {
                return new ResultDto<ICollection<BadgeDto>>(false, null, new[] { "Numai utilizatorii cu rolul de student pot avea insigne!" });
            }

            var badges = await _context.UserBadges
                .Where(ub => ub.UserId == userId)
                .Select(ub => ub.Badge)
                .ToListAsync();

            var badgesDto = _mapper.Map<ICollection<BadgeDto>>(badges);

            return new ResultDto<ICollection<BadgeDto>>(true, badgesDto, null);
        }

        public async Task<ResultDto<UserDto>> GetUserById(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<UserDto>(false, null, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var userDto = _mapper.Map<UserDto>(user);

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (role != null)
            {
                userDto.RoleName = role.Name;
            }

            return new ResultDto<UserDto>(true, userDto, null);
        }

        public async Task<ResultDto<ICollection<UserDto>>> GetUsers()
        {
            List<UserDto> userDtos = await _context.Users
                .Join(_context.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new { user, userRole })
                .Join(_context.Roles, role => role.userRole.RoleId, role => role.Id, (userUserRole, role) => new UserDto
                {
                    UserId = userUserRole.user.Id,
                    UserName = userUserRole.user.UserName,
                    Email = userUserRole.user.Email,
                    FirstName = userUserRole.user.FirstName,
                    LastName = userUserRole.user.LastName,
                    RoleId = role.Id,
                    RoleName = role.Name
                })
                .ToListAsync();

            return new ResultDto<ICollection<UserDto>>(true, userDtos, null);
        }
    }
}
