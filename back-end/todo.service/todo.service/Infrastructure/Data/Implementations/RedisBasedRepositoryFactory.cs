using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using todo.service.Infrastructure.Data.Interfaces;

namespace todo.service.Infrastructure.Data.Implementations
{
    public class RedisBasedRepositoryFactory : IRepositoryFactory
    {
        static readonly Dictionary<Type, object> createdRepository = new Dictionary<Type, object>();
        private readonly IServiceProvider services;

        public RedisBasedRepositoryFactory(IServiceProvider services)
        {
            this.services = services;
        }

        public IRepository<T> GetRepository<T>() where T : DataObject
        {
            if (!createdRepository.ContainsKey(typeof(T)))
            {
                createdRepository[typeof(T)] = new RedisBasedRepository<T>(services.GetRequiredService<ConnectionMultiplexer>().GetDatabase());
            }
            return createdRepository[typeof(T)] as IRepository<T>;
        }
    }
}
