using AutoMapper;
using todo.service.Services.Authentication.DTOs;
using todo.service.Services.Authentication.Interfaces;

namespace todo.service.Services.Authentication.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserInfoDto> GetUser(Guid userId)
        {
            return this.mapper.Map<UserInfoDto>(await this.userRepository.GetUser(userId));
        }

        public async Task<UserInfoDto> RegisterUser(UserDto dto)
        {
            return this.mapper.Map<UserInfoDto>(await userRepository.AddUser(dto.Username, dto.Password));
        }
    }
}
