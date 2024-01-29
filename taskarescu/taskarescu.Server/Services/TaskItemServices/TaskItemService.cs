using AutoMapper;
using Microsoft.EntityFrameworkCore;
using taskarescu.Server.Db;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;

namespace taskarescu.Server.Services.TaskItemServices
{
    public class TaskItemService : ITaskItemService
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public TaskItemService(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> AddTaskItem(Guid projectId, TaskItemDto taskItemDto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<int>(false, -1, new[] { "Proiectul nu a fost gasit!" });
            }

            if (taskItemDto.UserId != null)
            {
                var studentProject = await _context.StudentProjects
                   .FirstOrDefaultAsync(sp => sp.UserId == taskItemDto.UserId && sp.ProjectId == projectId);

                if (studentProject == null)
                {
                    return new ResultDto<int>(false, -1, new[] { "Task-ul poate fi asignat doar unui Student care face parte din proiect!" });
                }
            }

            if (taskItemDto.StatusId != null)
            {
                var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == taskItemDto.StatusId);

                if (status == null)
                {
                    return new ResultDto<int>(false, -1, new[] { "Statusul nu a fost gasit!" });
                }
            }

            var addedTaskItem = new TaskItem
            {
                Name = taskItemDto.Name,
                Description = taskItemDto.Description,
                ProjectId = projectId,
                Deadline = taskItemDto.Deadline,
                UserId = taskItemDto.UserId,
                StatusId = taskItemDto.StatusId
            };

            await _context.TaskItems.AddAsync(addedTaskItem);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<int>(false, -1, new[] { "Eroare la adaugarea task-ului!" });
            }

            return new ResultDto<int>(true, addedTaskItem.Id, null);
        }

        public async Task<ResultDto<bool>> DeleteTaskItemById(int taskId)
        {
            var taskItem = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId);

            if (taskItem == null) {
                return new ResultDto<bool>(false, false, new[] { "Task-ul nu a fost gasit" });
            }

            _context.TaskItems.Remove(taskItem);
            var no_changes = await _context.SaveChangesAsync();

            if (no_changes == 0)
            {
                return new ResultDto<bool>(false, false, new[] { "Eroare la stergerea task-ului" });
            }

            return new ResultDto<bool>(true, true, null);
        }

        public async Task<ResultDto<bool>> EditTaskItemById(Guid projectId, int taskId, TaskItemDto taskItemDto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Proiectul nu a fost gasit!" });
            }

            var taskItem = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId);
            
            if (taskItem == null)
            {
                return new ResultDto<bool>(false, false, new[] { "Task-ul nu a fost gasit!" });
            }

            if (taskItemDto.UserId != null)
            {
                var studentProject = await _context.StudentProjects
                    .FirstOrDefaultAsync(sp => sp.UserId == taskItemDto.UserId && sp.ProjectId == projectId);

                if (studentProject == null)
                {
                    return new ResultDto<bool>(false, false, new[] { "Task-ul poate fi asignat doar unui Student care face parte din proiect!" });
                }
            }

            if (taskItemDto.StatusId != null)
            {
                var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id ==  taskItemDto.StatusId);

                if (status == null)
                {
                    return new ResultDto<bool>(false, false, new[] { "Statusul nu a fost gasit!" });
                }
            }

            taskItem.Name = taskItemDto.Name;
            taskItem.Description = taskItemDto.Description;
            taskItem.Deadline = taskItemDto.Deadline;
            taskItem.UserId = taskItemDto.UserId;
            taskItem.StatusId = taskItemDto.StatusId;

            _context.TaskItems.Update(taskItem);
            _context.SaveChanges();

            return new ResultDto<bool>(true, true, null);          
        }

        public async Task<ResultDto<GetTaskItemDto>> GetTaskItemById(int taskId)
        {
            var taskItem = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId);

            if (taskItem == null) {
                return new ResultDto<GetTaskItemDto>(false, null, new[] { "Task-ul nu a fost gasit!" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == taskItem.UserId);
            var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == taskItem.StatusId);

            var taskItemDto = _mapper.Map<GetTaskItemDto>(taskItem);

            if (user != null)
            {
                taskItemDto.Username = user.UserName;
            }

            if (status != null)
            {
                taskItemDto.StatusName = status.Name;
            }

            return new ResultDto<GetTaskItemDto>(true, taskItemDto, null);
        }

        public async Task<ResultDto<ICollection<GetTaskItemDto>>> GetTaskItemsByProjectId(Guid projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return new ResultDto<ICollection<GetTaskItemDto>> (false, null, new[] { "Proiectul nu a fost gasit!" });
            }

            var taskItemDtos = await _context.TaskItems
                .Where(t => t.ProjectId == projectId)
                .Join(
                    _context.Users,
                    task => task.UserId,
                    user => user.Id,
                    (task, user) => new
                    {
                        Task = task,
                        User = user
                    })
                .Join(
                    _context.Statuses,
                    combined => combined.Task.StatusId,
                    status => status.Id,
                    (combined, status) => new GetTaskItemDto
                    {
                        Id = combined.Task.Id,
                        Name = combined.Task.Name,
                        Description = combined.Task.Description,
                        Deadline = combined.Task.Deadline,
                        UserId = combined.User.Id,
                        Username = combined.User.UserName,
                        StatusId = combined.Task.StatusId,
                        StatusName = status.Name
                    })
                .ToListAsync();


            return new ResultDto<ICollection<GetTaskItemDto>>(true, taskItemDtos, null);
        }
    }
}
