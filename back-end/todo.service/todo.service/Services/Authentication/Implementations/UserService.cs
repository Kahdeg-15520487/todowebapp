using todo.service.Services.Authentication.DTOs;
using todo.service.Services.Authentication.Interfaces;

namespace todo.service.Services.Authentication.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<bool> RegisterUser(UserDto dto)
        {
            return userRepository.AddUser(dto.Username, dto.Password);
        }
    }
}
