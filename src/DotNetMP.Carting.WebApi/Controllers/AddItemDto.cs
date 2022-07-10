namespace DotNetMP.Carting.WebApi.Controllers;

public class AddItemDto
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? ImageAltText { get; private set; }

    public AddItemDto(Guid id, string name, decimal price, int quantity, string imageUrl, string imageAltText)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        ImageUrl = imageUrl;
        ImageAltText = imageAltText;
    }
}
