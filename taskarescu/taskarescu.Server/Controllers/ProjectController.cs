using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskarescu.Server.DTOs;
using taskarescu.Server.Services.ProjectServices;

namespace taskarescu.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ProjectDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjectsByUserId(string userId)
        {
            var projectDtos = await _projectService.GetProjectsByUserId(userId);

            if (projectDtos == null)
            {
                return NotFound();
            }

            return Ok(projectDtos);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            var projectDto = await _projectService.GetProjectById(projectId);

            if (projectDto == null)
            {
                return NotFound();
            }

            return Ok(projectDto);
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProjectById(Guid projectId)
        {
            var isDeleted = await _projectService.DeleteProjectById(projectId);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProject(ProjectDto projectDto)
        {
            var projectId = await _projectService.AddProject(projectDto);

            return projectId != null ? Ok(projectId) : BadRequest();
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpPut("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProject(Guid projectId, ProjectPostDto projectPostDto)
        {
            var isEdited = await _projectService.EditProjectById(projectId, projectPostDto);

            if (!isEdited)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpPost("add-student/{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddStudentToProject(string username, Guid projectId)
        {
            var isAdded = await _projectService.AddStudentToProject(username, projectId);

            if (!isAdded)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Policy = "ProfsOnly")]
        [HttpDelete("remove-student/{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveStudentFromProject(string username, Guid projectId)
        {
            var isDeleted = await _projectService.RemoveStudentFromProject(username, projectId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
