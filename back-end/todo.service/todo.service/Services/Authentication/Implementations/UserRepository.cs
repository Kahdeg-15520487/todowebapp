using System.Collections.Generic;

using todo.service.Infrastructure.Data.Interfaces;
using todo.service.Services.Authentication.Data;
using todo.service.Services.Authentication.DTOs;
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

        public async Task<User> AddUser(string username, string password)
        {
            return await Task.FromResult(userRepo.Add(new User() { Id = Guid.NewGuid(), Username = username, Password = password }));
        }

        public Task<bool> Authenticate(string username, string password, out Guid userid)
        {
            var user = this.userRepo.GetByUserName(username);
            if (user?.Password == password)
            {
                userid = user.Id;
                return Task.FromResult(true);
            }
            userid = Guid.Empty;
            return Task.FromResult(false);
        }

        public Task<User> GetUser(Guid userId)
        {
            return Task.FromResult(this.userRepo.GetById(userId));
        }

        public async Task<IEnumerable<string>> GetUserNames()
        {
            return await Task.FromResult(this.userRepo.GetAll().Select(u => u.Username));
        }
    }
}
