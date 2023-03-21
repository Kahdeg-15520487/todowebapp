using Newtonsoft.Json;
using StackExchange.Redis;
using System.Reflection;
using todo.service.Infrastructure.Data.Interfaces;

namespace todo.service.Infrastructure.Data.Implementations
{
    public class RedisBasedRepository<T> : IRepository<T> where T : DataObject
    {
        private readonly IDatabase db;
        private readonly string collection;
        private readonly string keyList;

        public RedisBasedRepository(IDatabase db)
        {
            this.db = db;
            this.collection = typeof(T).Name;
            this.keyList = $"{this.collection}:all";
        }

        public IEnumerable<T> GetAll()
        {
            //return this.db.HashGetAll(this.collection).Select(h => ConvertFromRedis<T>(h));
            foreach (var userId in this.db.SetMembers(keyList).Select(v => v.ToString()))
            {
                yield return ConvertFromRedis<T>(this.db.HashGetAll($"{this.collection}:{userId}"));
            }
        }

        public T GetById(Guid id)
        {
            var key = $"{this.collection}:{id}";
            if (this.db.KeyExists(key))
            {
                return ConvertFromRedis<T>(this.db.HashGetAll(key));
            }
            throw new KeyNotFoundException(key);
        }

        public T Add(T entity)
        {
            var id = Guid.NewGuid();
            entity.Id = id;
            var key = $"{this.collection}:{id}";
            this.db.HashSet(key, ToHashEntries(entity));
            this.db.SetAdd(keyList, id.ToString());
            return GetById(id);
        }

        public bool Delete(Guid id)
        {
            var key = $"{this.collection}:{id}";
            if (this.db.KeyExists(key))
            {
                return this.db.KeyDelete(key);
            }
            throw new KeyNotFoundException(key);
        }

        public T Update(T entity)
        {
            var id = entity.Id;
            var key = $"{this.collection}:{id}";
            if (this.db.KeyExists(key))
            {
                this.db.HashSet(key, ToHashEntries(entity));
                return GetById(id);
            }
            throw new KeyNotFoundException(key);
        }

        private static HashEntry[] ToHashEntries(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            return properties
                .Where(x => x.GetValue(obj) != null) // <-- PREVENT NullReferenceException
                .Select
                (
                      property =>
                      {
                          object propertyValue = property.GetValue(obj);
                          string hashValue;

                          // This will detect if given property value is 
                          // enumerable, which is a good reason to serialize it
                          // as JSON!
                          if (propertyValue is IEnumerable<object>)
                          {
                              // So you use JSON.NET to serialize the property
                              // value as JSON
                              hashValue = JsonConvert.SerializeObject(propertyValue);
                          }
                          else
                          {
                              hashValue = propertyValue.ToString();
                          }

                          return new HashEntry(property.Name, hashValue);
                      }
                )
                .ToArray();
        }

        private static T ConvertFromRedis<T>(HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) continue;
                if (property.PropertyType != typeof(Guid))
                {
                    property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
                }
                else
                {
                    property.SetValue(obj, Convert.ChangeType(Guid.Parse(entry.Value.ToString()), property.PropertyType));
                }
            }
            return (T)obj;
        }
    }
}
