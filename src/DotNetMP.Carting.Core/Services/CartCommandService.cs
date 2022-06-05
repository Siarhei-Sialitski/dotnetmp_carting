using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.Carting.Core.Interfaces;
using DotNetMP.SharedKernel.Exceptions;
using DotNetMP.SharedKernel.Interfaces;

namespace DotNetMP.Carting.Core.Services;

internal class CartCommandService : ICartCommandService
{
    private readonly IRepository<Cart> _cartRepository;

    public CartCommandService(IRepository<Cart> cartRepository)
    {
        ArgumentNullException.ThrowIfNull(cartRepository, nameof(cartRepository));

        _cartRepository = cartRepository;
    }

    public async Task<Cart> AddItemToCartAsync(Guid cartId, Item item)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);

        if (cart == null)
        {
            cart = await _cartRepository.AddAsync(new Cart(cartId, new List<Item>()));
        }

        cart.AddItem(item);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.SaveChangesAsync();

        return cart;
    }

    public async Task<Cart> RemoveItemFromCartAsync(Guid cartId, Guid itemId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        if(cart == null)
        {
            throw new NotFoundException("Cart was not found.");
        }

        cart.RemoveItem(itemId);

        await _cartRepository.UpdateAsync(cart);
        await _cartRepository.SaveChangesAsync();

        return cart;
    }
}
