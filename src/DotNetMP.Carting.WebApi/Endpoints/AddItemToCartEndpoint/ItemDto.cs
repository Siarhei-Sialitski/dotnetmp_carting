using System.ComponentModel.DataAnnotations;

namespace DotNetMP.Carting.WebApi.Endpoints.AddItemToCartEndpoint;

public class ItemDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public ImageDto? Image { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}
