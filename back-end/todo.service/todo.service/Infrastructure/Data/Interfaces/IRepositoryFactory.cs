﻿namespace todo.service.Infrastructure.Data.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : DataObject;
    }
}