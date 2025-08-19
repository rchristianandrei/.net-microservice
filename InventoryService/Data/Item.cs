using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryService.Data
{
    public class Item(Guid itemId, int quantity)
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ItemId { get; set; } = itemId;

        public int Quantity { get; set; } = quantity;
    }
}
