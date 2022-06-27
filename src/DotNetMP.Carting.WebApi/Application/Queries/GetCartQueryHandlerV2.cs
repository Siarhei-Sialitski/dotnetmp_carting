using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.Carting.WebApi.Application.Models;
using DotNetMP.SharedKernel.Exceptions;
using DotNetMP.SharedKernel.Interfaces;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Queries;

public class GetCartQueryHandlerV2 : IRequestHandler<GetCartQueryV2, IList<ItemRecord>>
{
    private readonly IRepository<Cart> _cartRepository;

    public GetCartQueryHandlerV2(IRepository<Cart> cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<IList<ItemRecord>> Handle(GetCartQueryV2 request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.CartId);
        if (cart == null)
        {
            throw new NotFoundException();
        }


        var itemRecords = cart
            .Items
            .Select(i => new ItemRecord(i.Id, i.Name, i.Price, i.Quantity,
                i.Image != null ? new ImageRecord(i.Image.Url, i.Image.AltText) : null))
            .ToList();

        return itemRecords;
    }
}
