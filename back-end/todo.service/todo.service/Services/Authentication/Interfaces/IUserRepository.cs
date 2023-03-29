using todo.service.Services.Authentication.Data;
using todo.service.Services.Authentication.DTOs;

namespace todo.service.Services.Authentication.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(string username, string password);
        Task<bool> Authenticate(string username, string password, out Guid userid);
        Task<User> GetUser(Guid userId);
        Task<IEnumerable<string>> GetUserNames();
    }
}
