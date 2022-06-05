using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.Carting.Core.Interfaces;
using DotNetMP.SharedKernel.Interfaces;

namespace DotNetMP.Carting.Core.Services;
internal class CartQueryService : ICartQueryService
{
    private readonly IRepository<Cart> _cartRepository;

    public CartQueryService(IRepository<Cart> cartRepository)
    {
        ArgumentNullException.ThrowIfNull(cartRepository, nameof(cartRepository));

        _cartRepository = cartRepository;
    }

    public async Task<IEnumerable<Item>> GetCartItemsAsync(Guid cartId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);

        if (cart == null)
        {
            cart = await _cartRepository.AddAsync(new Cart(cartId, new List<Item>()));
        }

        return cart.Items.AsEnumerable();
    }
}
