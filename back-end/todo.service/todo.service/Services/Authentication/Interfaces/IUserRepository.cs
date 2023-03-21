namespace todo.service.Services.Authentication.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUser(string username, string password);
        Task<bool> Authenticate(string username, string password);
        Task<IEnumerable<string>> GetUserNames();
    }
}
