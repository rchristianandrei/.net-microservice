using MongoDB.Driver;

namespace Common;

public class MongoRepository<T>(IMongoDatabase database, string collectionName) : IMongoRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> entities = database.GetCollection<T>(collectionName);

    public async Task<IEnumerable<T>> GetAll()
    {
        return await (await entities.FindAsync(_ => true)).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter)
    {
        return await (await entities.FindAsync(filter)).ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await (await entities.FindAsync(T => T.Id == id)).FirstOrDefaultAsync();
    }

    public async Task<T> GetOne(FilterDefinition<T> filter)
    {
        return await (await entities.FindAsync(filter)).FirstOrDefaultAsync();
    }

    public async Task Create(T entity)
    {
        await entities.InsertOneAsync(entity);
    }

    public async Task Update(T entity)
    {
        await entities.ReplaceOneAsync(T => T.Id == T.Id, entity);
    }

    public async Task DeleteById(Guid id)
    {
        await entities.DeleteOneAsync(entity => entity.Id == id);
    }
}
