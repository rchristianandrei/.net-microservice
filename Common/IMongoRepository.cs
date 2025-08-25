using MongoDB.Driver;
using System.Linq.Expressions;

namespace Common
{
    public interface IMongoRepository<T> where T : IEntity
    {
        Task Create(T entity);
        Task DeleteById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter);
        Task<T> GetById(Guid id);
        Task<T> GetOne(Expression<Func<T, bool>> filter);
        Task Update(T entity);
    }
}