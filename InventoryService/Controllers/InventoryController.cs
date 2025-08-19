using Common;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(IMongoRepository<Inventory> itemRepository) : ControllerBase
    {
        private readonly IMongoRepository<Inventory> itemRepository = itemRepository;

        // GET: api/<InventoryController>
        [HttpGet]
        public async Task<IActionResult> Get(Guid userId)
        {
            var items = await itemRepository.GetOne(Builders<Inventory>.Filter.Eq(u => u.Id, userId));

            if (items == null) return NotFound();

            return Ok(items);
        }

        // GET api/<InventoryController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<InventoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Guid userId, IEnumerable<string> items)
        {
            await itemRepository.Create(new Inventory
            {
                Id = userId,
                Items = items
            });

            return NoContent();
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] IEnumerable<string> items)
        {
            var inventory = await itemRepository.GetOne(Builders<Inventory>.Filter.Eq(u => u.Id, userId));

            if (inventory == null) return NotFound();

            inventory.Items = items;

            return NoContent();
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var items = await itemRepository.GetOne(Builders<Inventory>.Filter.Eq(u => u.Id, userId));

            if (items == null) return NotFound();

            return NoContent();
        }
    }
}
