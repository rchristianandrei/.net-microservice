using Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryService.Data
{
    public class Inventory(Guid UserId) : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; } = UserId;

        public ICollection<Item> Items { get; set; } = [];
    }
}
