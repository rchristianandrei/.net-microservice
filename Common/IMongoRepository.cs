using MongoDB.Driver;

namespace Common
{
    public interface IMongoRepository<T> where T : IEntity
    {
        Task Create(T entity);
        Task DeleteById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter);
        Task<T> GetById(Guid id);
        Task<T> GetOne(FilterDefinition<T> filter);
        Task Update(T entity);
    }
}