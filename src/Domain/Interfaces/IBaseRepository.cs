
namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(int id);
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();

    }
}
