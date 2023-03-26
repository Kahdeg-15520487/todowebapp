namespace todo.service.Infrastructure.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Func<T, bool> query);
        T GetById(Guid id);
        T Add(T entity);
        T Update(T entity);
        bool Delete(Guid id);
    }
}
