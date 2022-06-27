using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.SharedKernel.Interfaces;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Commands;

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand>
{
    private readonly IRepository<Cart> _cartsRepository;

    public AddItemToCartCommandHandler(IRepository<Cart> cartsRepository)
    {
        _cartsRepository = cartsRepository;
    }

    public async Task<Unit> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        Image? image = null;
        if (!string.IsNullOrEmpty(request.ImageUrl) || !string.IsNullOrEmpty(request.ImageAltText))
        {
            image = new Image(request.ImageUrl, request.ImageAltText);
        }

        var item = new Item(request.ItemId, request.Name, request.Price, request.Quantity, image);

        var cart = await _cartsRepository.GetByIdAsync(request.CartId);
        if (cart == null)
        {
            await CreateCartWithItem(request.CartId, item);
        }
        else
        {
            await UpdateCartWithItem(cart, item);
        }

        return Unit.Value;
    }

    private async Task CreateCartWithItem(Guid cartId, Item item)
    {
        var cart = new Cart(cartId, new List<Item>() { item });
        await _cartsRepository.AddAsync(cart);
    }

    private async Task UpdateCartWithItem(Cart cart, Item item)
    {
        cart.AddItem(item);
        await _cartsRepository.UpdateAsync(cart);
    }
}
