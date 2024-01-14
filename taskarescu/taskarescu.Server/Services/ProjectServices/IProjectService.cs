using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<ICollection<ProjectDto>> GetProjectsByUserId(string userId);
        Task<ProjectDto> GetProjectById(Guid projectId);
        //Task<Guid> AddProject(ProjectPost projectPost);
        Task<bool> DeleteProjectById(Guid projectId);
    }
}
