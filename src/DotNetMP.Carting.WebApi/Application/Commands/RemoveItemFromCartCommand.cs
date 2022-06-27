using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Commands;

public class RemoveItemFromCartCommand : IRequest
{
    public Guid CartId { get; private set; }
    public Guid ItemId { get; private set; }

    public RemoveItemFromCartCommand(Guid cartId, Guid itemId)
    {
        CartId = cartId;
        ItemId = itemId;
    }
}
