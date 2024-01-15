using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Db;
using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
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
