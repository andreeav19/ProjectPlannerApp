﻿using taskarescu.Server.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
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
}