using StackExchange.Redis;
using todo.service.Infrastructure.Data.Implementations;
using todo.service.Infrastructure.Data.Interfaces;

namespace todo.service.Infrastructure.Data
{
    public static class ServiceRegistor
    {
        public static void RegisterDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("redis"));
            redis.GetDatabase().StringSet("lastStart", DateTime.Now.ToString());
            services.AddSingleton(redis);
            services.AddScoped<IDatabase>((serviceProvider) =>
            {
                var redisConnection = serviceProvider.GetService<ConnectionMultiplexer>();
                return redisConnection.GetDatabase();
            });

            services.AddSingleton<IRepositoryFactory, RedisBasedRepositoryFactory>();
        }
    }
}
