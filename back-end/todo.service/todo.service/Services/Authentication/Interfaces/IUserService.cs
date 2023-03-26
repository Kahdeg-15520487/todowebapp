﻿using todo.service.Services.Authentication.DTOs;

namespace todo.service.Services.Authentication.Interfaces
{
    public interface IUserService
    {
        Task<UserInfoDto> GetUser(Guid userId);
        Task<bool> RegisterUser(UserDto dto);
    }
}
