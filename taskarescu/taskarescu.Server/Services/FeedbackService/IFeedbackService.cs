using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.FeedbackService
{
    public interface IFeedbackService
    {
        Task<ResultDto<FeedbackGetDto>> GetFeedbackById(int feedbackId);
        Task<ResultDto<FeedbackGetDto>> GetFeedbackByTaskItemId(int taskItemId);
        Task<ResultDto<int>> AddFeedbackToTaskItem(string userId, int taskItemId, FeedbackDto feedbackDto);
        Task<ResultDto<bool>> EditFeedbackById(string userId, int taskItemId, FeedbackDto feedbackDto);
    }
}
