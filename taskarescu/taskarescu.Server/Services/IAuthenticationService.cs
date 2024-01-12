using System.IdentityModel.Tokens.Jwt;
using taskarescu.Server.DTOs;

namespace taskarescu.Server.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequest request);
    Task<string> Login(LoginRequest request);
}