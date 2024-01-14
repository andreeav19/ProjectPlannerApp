using taskarescu.Server.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using taskarescu.Server.Extensions;
using taskarescu.Server.Services.UserServices;

namespace taskarescu.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _usersService;

    public UsersController(IUserService usersService)
    {
        _usersService = usersService;
    }

    [Authorize(Policy = "UsersOnly")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public IActionResult GetUsers()
    {
        var response = _usersService.GetUsers();
        var resultDto = response.ToResultDto();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }
        return Ok(resultDto);
    }

    [Authorize(Policy = "UsersOnly")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetUserById(int id) {
        var response = _usersService.GetUserById(id);
        var resultDto = response.ToResultDto();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }
        return Ok(resultDto);
    }
}