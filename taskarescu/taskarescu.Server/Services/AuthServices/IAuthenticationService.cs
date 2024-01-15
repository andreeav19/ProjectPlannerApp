using System.IdentityModel.Tokens.Jwt;
using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services.AuthServices;
using FluentResults;
public interface IAuthenticationService
{
    Task<Result<string>> Register(RegisterRequestDto request);
    Task<Result<string>> Login(LoginRequest request);
}