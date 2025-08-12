using System.ComponentModel.DataAnnotations;

namespace ItemService.DTOs
{
    public record ItemDTO(Guid Id, string Name, string Description, double Price, DateTimeOffset DateTimeAdded);

    public record CreateItemDTO([Required] string Name, [Required]  string Description, [Range(0, double.PositiveInfinity)] double Price);

    public record UpdateItemDTO([Required] string Name, [Required]  string Description, [Range(0, double.PositiveInfinity)] double Price);
}
