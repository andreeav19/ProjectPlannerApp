using taskarescu.Server.DTOs;
using taskarescu.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using taskarescu.Server.Extensions;
namespace AuthenticationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public UsersController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authenticationService.Login(request);
        var resultDto = response.ToResultDto();

        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }


        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _authenticationService.Register(request);
        var resultDto = response.ToResultDto();
        if (!resultDto.IsSuccess)
        {
            return BadRequest(resultDto);
        }
        return Ok(response);
    }


}