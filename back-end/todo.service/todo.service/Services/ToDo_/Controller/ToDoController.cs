using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using todo.service.Services.Authentication.Data;
using todo.service.Services.Authentication.DTOs;
using todo.service.Services.Authentication.Interfaces;
using todo.service.Services.ToDo_.DTOs;
using todo.service.Services.ToDo_.Interfaces;

namespace todo.service.Services.ToDo_.Controller
{
    [Route("api/todo")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService todoService;

        public ToDoController(IToDoService todoService)
        {
            this.todoService = todoService;
        }

        [HttpGet]
        public async Task<IEnumerable<ToDoDto>> Get()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(CustomClaim.UserId).Value);
            return this.todoService.GetByUser(userId);
        }

        [HttpGet("{id}")]
        public async Task<ToDoDto> Get([FromRoute] Guid id)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(CustomClaim.UserId).Value);
            return this.todoService.GetById(id, userId);
        }

        [HttpPost]
        public async Task<ToDoDto> Create([FromBody] ToDoDto dto)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(CustomClaim.UserId).Value);
            return this.todoService.Create(dto, userId);
        }

        [HttpPut]
        public async Task<ToDoDto> Update([FromBody] ToDoDto dto)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(CustomClaim.UserId).Value);
            return this.todoService.Update(dto, userId);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete([FromRoute] Guid id)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(CustomClaim.UserId).Value);
            return this.todoService.Delete(id, userId);
        }
    }
}
