namespace InventoryService.DTOs
{
    public record ItemDto(Guid ItemId, string Name, string Description, int Quantity);

    public record AddItemDto(Guid ItemId, int QuantityToAdd);

    public record UpdateItemDto(Guid ItemId, int Quantity);

    public record DeleteItemDto(Guid ItemId);
}
