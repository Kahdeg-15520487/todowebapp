using todo.service.Infrastructure.Data.Interfaces;
using todo.service.Services.Authentication.Data;

namespace todo.service.Services.Authentication.Utility
{
    public static class UserRepositoryExtensionMethod
    {
        public static User GetByUserName(this IRepository<User> repository, string userName)
        {
            return repository.GetAll().FirstOrDefault(u => u.Username == userName);
        }
    }
}
