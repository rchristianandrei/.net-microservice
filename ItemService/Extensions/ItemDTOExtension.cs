using ItemService.Data;
using ItemService.DTOs;
using System.Runtime.CompilerServices;

namespace ItemService.Extensions
{
    public static class ItemDTOExtension
    {
        public static ItemDTO ToItemDTO(this Item item)
        {
            return new ItemDTO(item.Id, item.Name, item.Description, item.Price, item.DateTimeAdded);
        }
    }
}
