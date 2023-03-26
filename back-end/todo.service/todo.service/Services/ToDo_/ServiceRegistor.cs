using StackExchange.Redis;
using todo.service.Infrastructure.Data.Implementations;
using todo.service.Infrastructure.Data.Interfaces;
using todo.service.Services.ToDo_.Implementations;
using todo.service.Services.ToDo_.Interfaces;

namespace todo.service.Services.ToDo_
{
    public static class ServiceRegistor
    {
        public static void RegisterToDoServices(this IServiceCollection services)
        {
            services.AddScoped<IToDoService, ToDoService>();
        }
    }
}
