using Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ItemService.Data;

public class Item : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Price { get; set; }

    public DateTimeOffset DateTimeAdded { get; set; }
}
