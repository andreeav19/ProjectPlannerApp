using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Db;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;

namespace taskarescu.Server.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> DeleteProjectById(Guid projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ProjectDto> GetProjectById(Guid projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ICollection<ProjectDto>> GetProjectsByUserId(string userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                var userRole = await _context.Roles.FindAsync(user.RoleId);

                if (userRole != null)
                {
                    ICollection<Project> projects;

                    if (userRole.Name != "Student")
                    {
                        projects = await _context.Projects
                            .Where(p => p.UserId == userId)
                            .ToListAsync();
                    }

                    projects = await _context.StudentProjects
                        .Where(sp => sp.UserId == userId)
                        .Select(sp => sp.Project)
                        .ToListAsync();

                    return _mapper.Map<ICollection<ProjectDto>>(projects);

                }
            }

            return null;
        }
    }
}
