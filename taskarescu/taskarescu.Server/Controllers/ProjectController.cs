﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskarescu.Server.DTOs;
using taskarescu.Server.Models;
using taskarescu.Server.Services.FeedbackService;
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
        private readonly IFeedbackService _feedbackService;

        public ProjectController(IProjectService projectService, ITaskItemService taskItemService, IFeedbackService feedbackService)
        {
            _projectService = projectService;
            _taskItemService = taskItemService;
            _feedbackService = feedbackService;
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("/Project/users/{userId}")]
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
        [HttpPost("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<Guid>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProject(string userId, [FromBody] ProjectPostDto projectDto)
        {
            var resultDto = await _projectService.AddProject(userId, projectDto);

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
        [HttpPost("{projectId}/students/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStudentToProject(string username, Guid projectId)
        {
            var resultDto = await _projectService.AddStudentToProject(username, projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpDelete("{projectId}/students/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveStudentFromProject(string username, Guid projectId)
        {
            var resultDto = await _projectService.RemoveStudentFromProject(username, projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("{projectId}/tasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<GetTaskItemDto>>))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<GetTaskItemDto>))]
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
        public async Task<IActionResult> AddTaskItem(Guid projectId, [FromBody] PutPostTaskItemDto taskItemDto)
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
        public async Task<IActionResult> EditTaskItemById(Guid projectId, int taskId, [FromBody] PutPostTaskItemDto taskItemDto)
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

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("/feedback/{feedbackId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<FeedbackGetDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFeedbackById(int feedbackId)
        {
            var resultDto = await _feedbackService.GetFeedbackById(feedbackId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("/tasks/{taskItemId}/feedback")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<FeedbackGetDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFeedbackByTaskItemId(int taskItemId)
        {
            var resultDto = await _feedbackService.GetFeedbackByTaskItemId(taskItemId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("{projectId}/students")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<string>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStudentUsernamesByProjectId(Guid projectId)
        {
            var resultDto = await _projectService.GetStudentsByProjectId(projectId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    }
}
