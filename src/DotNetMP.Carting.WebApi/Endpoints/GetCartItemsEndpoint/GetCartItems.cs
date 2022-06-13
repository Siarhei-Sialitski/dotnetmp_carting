using Ardalis.ApiEndpoints;
using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.GetCartItemsEndpoint;

public class GetCartItems : EndpointBaseAsync
  .WithRequest<GetCartItemsRequest>
  .WithActionResult<GetCartItemsResponse>
{
    private readonly IRepository<Cart> _cartRepository;

    public GetCartItems(IRepository<Cart> cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpGet(GetCartItemsRequest.Route)]
    public async override Task<ActionResult<GetCartItemsResponse>> HandleAsync(GetCartItemsRequest request, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByIdAsync(request.CartId);
        if (cart == null)
        {
            return NotFound();
        }


        var itemRecords = cart
            .Items
            .Select(i => new ItemRecord(i.Id, i.Name, i.Price, i.Quantity, 
                i.Image != null ? new ImageRecord(i.Image.Url, i.Image.AltText) : null));

        return new GetCartItemsResponse
        {
            Items = itemRecords.ToList()
        };
    }
}
