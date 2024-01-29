using AutoMapper;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Db;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;

namespace taskarescu.Server.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FeedbackService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> AddFeedbackToTaskItem(string userId, int taskItemId, FeedbackDto feedbackDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<int>(false, -1, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);

            if (role.Name == "Student")
            {
                return new ResultDto<int>(false, -1, new[] { "Un student nu poate acorda feedback!" });
            }

            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskItemId);

            if (task == null)
            {
                return new ResultDto<int>(false, -1, new[] { "Task-ul nu a fost gasit!" });
            }

            var difficulty = await _context.TaskItems.FirstOrDefaultAsync(d => d.Id == feedbackDto.DifficultyId);

            if (difficulty == null)
            {
                return new ResultDto<int>(false, -1, new[] { "Dificultatea nu a fost gasita!" });
            }

            var feedback = new Feedback
            {
                Description = feedbackDto.Description,
                Points = feedbackDto.Points,
                UserId = userId,
                TaskItemId = taskItemId,
                DifficultyId = feedbackDto.DifficultyId
            };

            await _context.Feedbacks.AddAsync(feedback);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<int>(false, -1, new[] { "Eroare la adaugarea feedback-ului!" });
            }

            return new ResultDto<int>(true, feedback.Id, null);
        }

        public async Task<ResultDto<bool>> EditFeedbackById(string userId, int feedbackId, FeedbackDto feedbackDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Utilizatorul nu a fost gasit!" });
            }

            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedbackId);

            if (feedback == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Feedback-ul nu a fost gasit!" });
            }

            if (userId != feedback.UserId)
            {
                return new ResultDto<bool>(false, false, new[] { "Un utilizator poate modifica doar feedback-ul creat de el!" });
            }

            var difficulty = await _context.TaskItems.FirstOrDefaultAsync(d => d.Id == feedbackDto.DifficultyId);

            if (difficulty == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Dificultatea nu a fost gasita!" });
            }

            feedback.Description = feedbackDto.Description;
            feedback.Points = feedbackDto.Points;
            feedback.DifficultyId = feedbackDto.DifficultyId;

            _context.Update(feedback);
            await _context.SaveChangesAsync();

            return new ResultDto<bool>(true, true, null);
        }

        public async Task<ResultDto<FeedbackGetDto>> GetFeedbackById(int feedbackId)
        {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedbackId);
            var feedbackDto = _mapper.Map<FeedbackGetDto>(feedback);

            if (feedback == null)
            {
                return new ResultDto<FeedbackGetDto>(false, null, new[] { "Feedback-ul nu a fost gasit!" });
            }

            var difficulty = await _context.Difficulties.FirstOrDefaultAsync(d => d.Id == feedback.DifficultyId);

            if (difficulty != null) { 
                feedbackDto.DifficultyName = difficulty.Name;
            }

            return new ResultDto<FeedbackGetDto>(true, feedbackDto, null);
        }

        public async Task<ResultDto<FeedbackGetDto>> GetFeedbackByTaskItemId(int taskItemId)
        {
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.TaskItemId == taskItemId);
            var feedbackDto = _mapper.Map<FeedbackGetDto>(feedback);

            if (feedback == null)
            {
                return new ResultDto<FeedbackGetDto>(false, null, new[] { "Feedback-ul nu a fost gasit!" });
            }

            var difficulty = await _context.Difficulties.FirstOrDefaultAsync(d => d.Id == feedback.DifficultyId);

            if (difficulty != null)
            {
                feedbackDto.DifficultyName = difficulty.Name;
            }

            return new ResultDto<FeedbackGetDto>(true, feedbackDto, null);
        }
    }
}
