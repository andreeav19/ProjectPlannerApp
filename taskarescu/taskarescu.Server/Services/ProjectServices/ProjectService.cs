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

        public async Task<Guid> AddProject(ProjectDto projectDto)
        {
            var projectId = Guid.NewGuid();

            var project = new Project
            {
                Id = projectId,
                Name = projectDto.Name,
                Description = projectDto.Description,
                UserId = projectDto.UserId
            };
            
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();

            return projectId;
        }

        public async Task<bool> AddStudentToProject(string username, Guid projectId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            var userRole = user != null ? await _context.Roles.FindAsync(user.Id) : null;

            if (userRole == null || userRole.Name != "Student")
            {
                return false;
            }

            var studentProject = new StudentProject
            {
                UserId = user.Id,
                ProjectId = projectId
            };

            await _context.StudentProjects.AddAsync(studentProject);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return false;
            }

            return true;
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

        public async Task<bool> EditProjectById(Guid projectId, ProjectPostDto projectPostDto)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return false;
            }

            project.Name = projectPostDto.Name;
            project.Description = projectPostDto.Description;

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

        public async Task<bool> RemoveStudentFromProject(string username, Guid projectId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            var project = await _context.Projects.FindAsync(projectId);

            if (user == null || project == null) {
                return false;
            }

            var studentProject = await _context.StudentProjects.FirstOrDefaultAsync(sp => sp.UserId == user.Id && sp.ProjectId == projectId);

            _context.StudentProjects.Remove(studentProject);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
