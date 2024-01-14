using FluentResults;

namespace taskarescu.Server.Services.UserServices
{
    public interface IUserService
    {
        Task<Result<string>> GetUsers();
        Task<Result<string>> GetUserById(int id);
    }
}
