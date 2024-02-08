using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<ResultDto<ICollection<ProjectDto>>> GetProjectsByUserId(string userId);
        Task<ResultDto<ProjectDto>> GetProjectById(Guid projectId);
        Task<ResultDto<Guid>> AddProject(string userId, ProjectPostDto projectDto);
        Task<ResultDto<bool>> EditProjectById(Guid projectId, ProjectPostDto projectPostDto);
        Task<ResultDto<bool>> DeleteProjectById(Guid projectId);
        Task<ResultDto<bool>> AddStudentToProject(string username, Guid projectId);
        Task<ResultDto<bool>> RemoveStudentFromProject(string username, Guid projectId);
        Task<ResultDto<ICollection<string>>> GetStudentsByProjectId(Guid projectId);
    }
}
