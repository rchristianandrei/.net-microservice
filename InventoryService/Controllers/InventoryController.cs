using Common;
using InventoryService.Data;
using InventoryService.DTOs;
using InventoryService.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(IMongoRepository<Inventory> itemRepository) : ControllerBase
    {
        private readonly IMongoRepository<Inventory> itemRepository = itemRepository;

        // GET: api/<InventoryController>
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var inventory = await FindInventory(userId);

            if (inventory == null) return NotFound();

            return Ok(inventory.Items.Select(i => i.ToItemDto()));
        }

        // POST api/<InventoryController>
        [HttpPost]
        public async Task<IActionResult> Post(Guid userId, [FromBody] AddItemDto addItemDto)
        {
            var inventory = await FindInventory(userId);

            if (inventory == null)
            {
                inventory = new Inventory(userId);
                await itemRepository.Create(inventory);
            }

            var item = FindItem(inventory, addItemDto.ItemId);

            if (item == null)
            {
                inventory.Items.Add(new Item(addItemDto.ItemId,addItemDto.QuantityToAdd));
            }
            else
            {
                item.Quantity += addItemDto.QuantityToAdd;
            }

            await itemRepository.Update(inventory);

            return NoContent();
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UpdateItemDto updateItemDto)
        {
            var inventory = await FindInventory(userId);

            if (inventory == null) return NotFound();

            var item = FindItem(inventory, updateItemDto.ItemId);

            if (item == null) return NotFound();

            item.Quantity = updateItemDto.Quantity;

            await itemRepository.Update(inventory);

            return NoContent();
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId, [FromBody] DeleteItemDto deleteItemDto)
        {
            var inventory = await FindInventory(userId);

            if (inventory == null) return NotFound();

            var item = FindItem(inventory, deleteItemDto.ItemId);

            if(item == null) return NotFound();

            inventory.Items.Remove(item);

            await itemRepository.Update(inventory);

            return NoContent();
        }

        private async Task<Inventory?> FindInventory(Guid userId)
        {
            return await itemRepository.GetOne(u => u.UserId == userId); ;
        }

        private static Item? FindItem(Inventory inventory, Guid itemId)
        {
            return inventory.Items.FirstOrDefault(item => item.ItemId == itemId);
        }
    }
}
