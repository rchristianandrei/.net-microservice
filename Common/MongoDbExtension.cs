using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Common;

public static class MongoDbExtension
{
    public static IServiceCollection AddMongoDb<T>(this IServiceCollection services, IConfiguration configuration) where T : IEntity
    {
        services.AddSingleton(configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>() ??
            throw new InvalidOperationException("MongoDBSettings section is missing or invalid"));

        services.AddSingleton<IMongoRepository<T>>(service =>
        {
            var settings = service.GetRequiredService<MongoDbSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            return new MongoRepository<T>(database, settings.CollectionName);
        });
        return services;
    }
}
