using InventoryService.Data;
using InventoryService.DTOs;
using System.Runtime.CompilerServices;

namespace InventoryService.Extensions
{
    public static class ItemDtoExtension
    {
        public static ItemDto ToItemDto(this Item item)
        {
            return new ItemDto(item.ItemId, "<Name>", "<Description>", item.Quantity);
        }
    }
}
