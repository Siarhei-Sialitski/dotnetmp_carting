using DotNetMP.Carting.WebApi.Application.Models;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Queries;

public class GetCartQueryV2 : IRequest<IList<ItemRecord>>
{
    public Guid CartId { get; private set; }

    public GetCartQueryV2(Guid cartId)
    {
        CartId = cartId;
    }
}