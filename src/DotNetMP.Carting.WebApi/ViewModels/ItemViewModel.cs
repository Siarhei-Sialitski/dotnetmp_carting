using System.ComponentModel.DataAnnotations;

namespace DotNetMP.Carting.WebApi.ViewModels;

public class ItemViewModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public ImageViewModel? Image { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}
