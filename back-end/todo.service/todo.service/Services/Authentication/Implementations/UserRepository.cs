using System.Collections.Generic;
using todo.service.Infrastructure.Data.Interfaces;
using todo.service.Services.Authentication.Data;
using todo.service.Services.Authentication.Interfaces;
using todo.service.Services.Authentication.Utility;

namespace todo.service.Services.Authentication.Implementations
{
    public class UserRepository : IUserRepository
    {
        private IRepository<User> userRepo;

        public UserRepository(IRepositoryFactory repositoryFactory)
        {
            this.userRepo = repositoryFactory.GetRepository<User>();
        }

        public async Task<bool> AddUser(string username, string password)
        {
            return await Task.FromResult(userRepo.Add(new User() { Username = username, Password = password }) != null);
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            if (await Task.FromResult(this.userRepo.GetByUserName(username)?.Password == password))
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<string>> GetUserNames()
        {
            return await Task.FromResult(this.userRepo.GetAll().Select(u => u.Username));
        }
    }
}
