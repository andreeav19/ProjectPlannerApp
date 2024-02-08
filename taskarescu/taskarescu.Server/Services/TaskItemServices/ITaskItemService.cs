using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.TaskItemServices
{
    public interface ITaskItemService
    {
        Task<ResultDto<ICollection<GetTaskItemDto>>> GetTaskItemsByProjectId(Guid projectId);
        Task<ResultDto<GetTaskItemDto>> GetTaskItemById(int taskId);
        Task<ResultDto<int>> AddTaskItem(Guid projectId, PutPostTaskItemDto taskItemDto);
        Task<ResultDto<bool>> EditTaskItemById(Guid projectId, int taskId, PutPostTaskItemDto taskItemDto);
        Task<ResultDto<bool>> DeleteTaskItemById(int taskId);
    }
}
