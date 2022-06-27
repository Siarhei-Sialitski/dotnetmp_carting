using DotNetMP.Carting.WebApi.Application.Models;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Queries;

public class GetCartQueryV1 : IRequest<CartRecord>
{
    public Guid CartId { get; private set; }

    public GetCartQueryV1(Guid cartId)
    {
        CartId = cartId;
    }
}
