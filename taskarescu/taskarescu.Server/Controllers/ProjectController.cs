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
    }
}
