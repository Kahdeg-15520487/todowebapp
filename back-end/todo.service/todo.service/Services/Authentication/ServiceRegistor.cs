

using Microsoft.AspNetCore.Authentication;
using todo.service.Services.Authentication.Implementations;
using todo.service.Services.Authentication.Interfaces;

namespace todo.service.Services.Authentication
{
    public static class ServiceRegistor
    {
        public static void RegisterAuthenticationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddAuthentication("BasicAuthentication").AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }
    }
}
