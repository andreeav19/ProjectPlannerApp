using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<ResultDto<ICollection<string>>> GetStudentsByProjectId(Guid projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<ICollection<string>>(false, null, new[] { "Proiectul nu a fost gasit! " });
            }

            var usernames = await _context.StudentProjects
                .Where(sp => sp.ProjectId == projectId)
                .Join(
                    _context.Users,
                    studentProject => studentProject.UserId,
                    user => user.Id,
                    (studentProject, user) => user.UserName
                )
                .ToListAsync();

            return new ResultDto<ICollection<string>>(true, usernames, null);
        }

        async Task<ResultDto<Guid>> IProjectService.AddProject(ProjectDto projectDto)
        {
            var projectId = Guid.NewGuid();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == projectDto.UserId);

            if (user == null)
            {
                return new ResultDto<Guid>(false, Guid.Empty, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (userRole.Name == "Student")
            {
                return new ResultDto<Guid>(false, Guid.Empty, new[] { "Utilizatorul nu are rolul de Admin sau Profesor!" });
            }

            var project = new Project
            {
                Id = projectId,
                Name = projectDto.Name,
                Description = projectDto.Description,
                UserId = projectDto.UserId
            };

            await _context.AddAsync(project);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<Guid>(false, Guid.Empty, new[] { "Eroare la crearea unui proiect!" });
            }

            return new ResultDto<Guid>(true, projectId, null);
            
        }

        async Task<ResultDto<bool>> IProjectService.AddStudentToProject(string userId, Guid projectId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (userRole.Name != "Student")
            {
                return new ResultDto<bool>(false, false, new[] { "Utilizatorul nu poate fi adaugat deoarece nu are rolul de student!" });
            }

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Proiectul la care incercati sa adaugati studenti nu a fost gasit!" } );
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
                return new ResultDto<bool>(false, false, new[] { "Eroare la crearea asocierii de tip proiect-student!" } );
            }

            return new ResultDto<bool>(true, true, null);
        }

        async Task<ResultDto<bool>> IProjectService.DeleteProjectById(Guid projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Proiectul nu a fost gasit!" });
            }

            _context.Projects.Remove(project);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<bool>(false, false, new[] { "Eroare la stergerea proiectului!" });
            }

            return new ResultDto<bool>(true, true, null);
        }

        async Task<ResultDto<bool>> IProjectService.EditProjectById(Guid projectId, ProjectPostDto projectPostDto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Proiectul nu a fost gasit!" });
            }

            project.Name = projectPostDto.Name;
            project.Description = projectPostDto.Description;

            await _context.SaveChangesAsync();

            return new ResultDto<bool>(true, true, null);
        }

        async Task<ResultDto<ProjectDto>> IProjectService.GetProjectById(Guid projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return new ResultDto<ProjectDto>(false, null, new[] { "Proiectul nu a fost gasit!" });
            }

            var projectDto = _mapper.Map<ProjectDto>(project);

            return new ResultDto<ProjectDto>(true, projectDto, null);
        }

        async Task<ResultDto<ICollection<ProjectDto>>> IProjectService.GetProjectsByUserId(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<ICollection<ProjectDto>>(false, null, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            ICollection<Project> projects;

            if (userRole.Name != "Student")
            {
                projects = await _context.Projects
                    .Where(p => p.UserId == userId)
                    .ToListAsync();
            } else
            {
                projects = await _context.StudentProjects
                .Where(sp => sp.UserId == userId)
                .Select(sp => sp.Project)
                .ToListAsync();
            }

            var projectDtos = _mapper.Map<ICollection<ProjectDto>>(projects);

            return new ResultDto<ICollection<ProjectDto>>(true, projectDtos, null);
        }

        async Task<ResultDto<bool>> IProjectService.RemoveStudentFromProject(string userId, Guid projectId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Proiectul nu a fost gasit!" });
            }

            var studentProject = await _context.StudentProjects.FirstOrDefaultAsync(sp => sp.UserId == user.Id && sp.ProjectId == projectId);

            if (studentProject == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Asocierea dintre student si proiect nu a fost gasita!" });
            }

            _context.StudentProjects.Remove(studentProject);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<bool>(false, false, new[] { "Eroare la inlaturarea studentului din echipa!" });
            }

            return new ResultDto<bool>(true, true, null);
        }
    }
}
