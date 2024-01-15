using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskarescu.Server.DTOs;
using taskarescu.Server.Services.ProjectServices;
using taskarescu.Server.Services.TaskItemServices;

namespace taskarescu.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ITaskItemService _taskItemService;

        public ProjectController(IProjectService projectService, ITaskItemService taskItemService)
        {
            _projectService = projectService;
            _taskItemService = taskItemService;
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<ProjectDto>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProjectsByUserId(string userId)
        {
            var resultDto = await _projectService.GetProjectsByUserId(userId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ProjectDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            var resultDto = await _projectService.GetProjectById(projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProjectById(Guid projectId)
        {
            var resultDto = await _projectService.DeleteProjectById(projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<Guid>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProject([FromBody] ProjectDto projectDto)
        {
            var resultDto = await _projectService.AddProject(projectDto);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpPut("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditProject(Guid projectId, [FromBody] ProjectPostDto projectPostDto)
        {
            var resultDto = await _projectService.EditProjectById(projectId, projectPostDto);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpPost("{projectId}/students/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStudentToProject(string userId, Guid projectId)
        {
            var resultDto = await _projectService.AddStudentToProject(userId, projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpDelete("{projectId}/students/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveStudentFromProject(string userId, Guid projectId)
        {
            var resultDto = await _projectService.RemoveStudentFromProject(userId, projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("{projectId}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<TaskItemDto>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTaskItemsByProjectId(Guid projectId)
        {
            var resultDto = await _taskItemService.GetTaskItemsByProjectId(projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("/tasks/{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<TaskItemDto>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTaskItemById(int taskId)
        {
            var resultDto = await _taskItemService.GetTaskItemById(taskId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "StudentsOnly")]
        [HttpPost("{projectId}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<int>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTaskItem(Guid projectId, [FromBody] TaskItemDto taskItemDto)
        {
            var resultDto = await _taskItemService.AddTaskItem(projectId, taskItemDto);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "StudentsOnly")]
        [HttpPut("{projectId}/tasks/{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditTaskItemById(Guid projectId, int taskId, [FromBody] TaskItemDto taskItemDto)
        {
            var resultDto = await _taskItemService.EditTaskItemById(projectId, taskId, taskItemDto);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "StudentsOnly")]
        [HttpDelete("/tasks/{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTaskItemById(int taskId)
        {
            var resultDto = await _taskItemService.DeleteTaskItemById(taskId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    }
}
