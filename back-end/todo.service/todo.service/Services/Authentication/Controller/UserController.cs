﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using todo.service.Services.Authentication.DTOs;
using todo.service.Services.Authentication.Interfaces;

namespace todo.service.Services.Authentication.Controller
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> RegisterUser([FromBody] UserDto userDto)
        {
            return await userService.RegisterUser(userDto);
        }
    }
}
