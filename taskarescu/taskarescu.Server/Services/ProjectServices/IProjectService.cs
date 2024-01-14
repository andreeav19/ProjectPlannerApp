using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<ICollection<ProjectDto>> GetProjectsByUserId(string userId);
        Task<ProjectDto> GetProjectById(Guid projectId);
        Task<Guid> AddProject(ProjectDto projectDto);
        Task<bool> EditProjectById(Guid projectId, ProjectPostDto projectPostDto);
        Task<bool> DeleteProjectById(Guid projectId);
        Task<bool> AddStudentToProject(string username, Guid projectId);
        Task<bool> RemoveStudentFromProject(string username, Guid projectId);

    }
}
