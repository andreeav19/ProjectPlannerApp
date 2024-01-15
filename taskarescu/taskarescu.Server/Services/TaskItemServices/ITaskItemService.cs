using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.TaskItemServices
{
    public interface ITaskItemService
    {
        Task<ResultDto<ICollection<TaskItemDto>>> GetTaskItemsByProjectId(Guid projectId);
        Task<ResultDto<TaskItemDto>> GetTaskItemById(int taskId);
        Task<ResultDto<int>> AddTaskItem(Guid projectId, TaskItemDto taskItemDto);
        Task<ResultDto<bool>> EditTaskItemById(Guid projectId, int taskId, TaskItemDto taskItemDto);
        Task<ResultDto<bool>> DeleteTaskItemById(int taskId);
    }
}
