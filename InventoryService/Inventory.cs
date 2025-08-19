using Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryService
{
    public class Inventory : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public IEnumerable<string> Items { get; set; } = [];
    }
}
