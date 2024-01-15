using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskarescu.Server.DTOs;
using taskarescu.Server.Services.BadgeServices;

namespace taskarescu.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BadgeController : Controller
    {
        private readonly IBadgeService _badgeService;

        public BadgeController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<BadgeDto>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBadges()
        {
            var resultDto = await _badgeService.GetBadges();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "UsersOnly")]
        [HttpGet("{badgeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<BadgeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBadgeById(int badgeId)
        {
            var resultDto = await _badgeService.GetBadgeById(badgeId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "AdminsOnly")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<int>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBadge(BadgeDto badgeDto)
        {
            var resultDto = await _badgeService.AddBadge(badgeDto);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "AdminsOnly")]
        [HttpPut("{badgeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditBadgeById(int badgeId, [FromBody] BadgeDto badgeDto)
        {
            var resultDto = await _badgeService.EditBadgeById(badgeId, badgeDto);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [Authorize(Policy = "AdminsOnly")]
        [HttpDelete("{badgeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBadgeById(int badgeId)
        {
            var resultDto = await _badgeService.DeleteBadgeById(badgeId);

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    }
}
