using DotNetMP.Carting.Core.Aggregates.CartAggregate;

namespace DotNetMP.Carting.Core.Interfaces;

public  interface ICartCommandService
{
    Task<Cart> AddItemToCartAsync(Guid cartId, Item item);
    Task<Cart> RemoveItemFromCartAsync(Guid cartId, Guid itemId);
}
