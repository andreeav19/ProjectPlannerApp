using taskarescu.Server.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskarescu.Server.Services.UserServices;
using Azure;
using taskarescu.Server.Extensions;
using taskarescu.Server.Services.BadgeServices;

namespace taskarescu.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _usersService;
    private readonly IBadgeService _badgeService;

    public UsersController(IUserService usersService)
    {
        _usersService = usersService;
    }

    [Authorize(Policy = "UsersOnly")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<UserDto>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers()
    {
        var resultDto = await _usersService.GetUsers();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }
        return Ok(resultDto);
    }

    [Authorize(Policy = "UsersOnly")]
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<UserDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserById(string userId) {
        var resultDto = await _usersService.GetUserById(userId);

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }
        return Ok(resultDto);
    }

    [Authorize(Policy = "AdminsOnly")]
    [HttpPost("{userId}/role")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditUserRole(string userId, [FromBody] string roleName)
    {
        var response = await _usersService.EditUserRole(userId, roleName);
        var resultDto = response.ToResultDto();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }

        return Ok(response);
    }

    [Authorize(Policy = "UsersOnly")]
    [HttpGet("{userId}/badges")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<BadgeDto>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserBadges(string userId)
    {
        var resultDto = await _usersService.GetBadgesByUserId(userId);

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }
        return Ok(resultDto);
    }

    [Authorize(Policy = "ProfsOnly")]
    [HttpPost("{userId}/badges/{badgeId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<bool>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBadgeToUser(int badgeId, string userId)
    {
        var resultDto = await _badgeService.AddBadgeToUser(badgeId, userId);

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }

        return Ok(resultDto);
    }

    //[HttpGet("leaderboard")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<ICollection<UserScoreDto>>))]
    //public async Task<IActionResult> GetLeaderboardUsers()
    //{
    //    var resultDto = await _usersService.GetLeaderBoardUsers();

    //    if (!resultDto.IsSuccess)
    //    {
    //        return BadRequest(resultDto);
    //    }
    //    return Ok(resultDto);
    //}

}