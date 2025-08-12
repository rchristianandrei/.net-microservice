using Common;
using ItemService.Data;
using ItemService.DTOs;
using ItemService.Extensions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController(IMongoRepository<Item> itemRepository) : ControllerBase
{
    private readonly IMongoRepository<Item> itemRepository = itemRepository;

    // GET: api/<ItemController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var temp = (await this.itemRepository.GetAll()).Select(item => item.ToItemDTO());
        return Ok(temp);
    }

    // GET api/<ItemController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await this.itemRepository.GetById(id);

        if (item == null) return NotFound();

        return Ok(item.ToItemDTO());
    }

    // POST api/<ItemController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateItemDTO value)
    {
        await this.itemRepository.Create(new Item
        {
            Id = new Guid(),
            Name = value.Name,
            Description = value.Description,
            Price = value.Price,
            DateTimeAdded = DateTimeOffset.Now
        });
        return NoContent();
    }

    // PUT api/<ItemController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateItemDTO value)
    {
        var item = await this.itemRepository.GetById(id);

        if (item == null) return NotFound();

        item.Name = value.Name;
        item.Description = value.Description;
        item.Price = value.Price;

        await this.itemRepository.Update(item);

        return NoContent();
    }

    // DELETE api/<ItemController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await this.itemRepository.GetById(id);

        if (item == null) return NotFound();

        await this.itemRepository.DeleteById(id);

        return NoContent();
    }
}
