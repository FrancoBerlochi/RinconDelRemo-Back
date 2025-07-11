﻿
namespace Domain.Interfaces
{
    public interface IBaseRepository<T, TId> where T : class
    {
        T? GetById(TId id);
        List<T> GetAll();
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();

    }
}
