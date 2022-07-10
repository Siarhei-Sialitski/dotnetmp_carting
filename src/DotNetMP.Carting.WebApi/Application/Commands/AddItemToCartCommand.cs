using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Commands;

public class AddItemToCartCommand : IRequest
{
    public Guid CartId { get; private set; }
    public Guid ItemId { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string ImageUrl { get; private set; }
    public string ImageAltText { get; private set; }

    public AddItemToCartCommand(Guid cartId, Guid itemId, string name, decimal price, int quantity, string imageUrl, string imageAltText)
    {
        CartId = cartId;
        ItemId = itemId;
        Name = name;
        Price = price;
        Quantity = quantity;
        ImageUrl = imageUrl;
        ImageAltText = imageAltText;
    }
}