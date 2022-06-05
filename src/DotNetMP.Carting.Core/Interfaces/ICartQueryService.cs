using DotNetMP.Carting.Core.Aggregates.CartAggregate;

namespace DotNetMP.Carting.Core.Interfaces;

public interface ICartQueryService
{
    Task<IEnumerable<Item>> GetCartItemsAsync(Guid cartId);
}
