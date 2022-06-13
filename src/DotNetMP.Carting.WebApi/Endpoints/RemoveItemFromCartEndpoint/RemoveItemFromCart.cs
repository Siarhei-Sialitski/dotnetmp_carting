using Ardalis.ApiEndpoints;
using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.RemoveItemFromCartEndpoint;

public class RemoveItemFromCart : EndpointBaseAsync
  .WithRequest<RemoveItemFromCartRequest>
  .WithActionResult
{
    private readonly IRepository<Cart> _cartRepository;

    public RemoveItemFromCart(IRepository<Cart> cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpDelete(RemoveItemFromCartRequest.Route)]
    public async override Task<ActionResult> HandleAsync(RemoveItemFromCartRequest request, CancellationToken cancellationToken = default)
    {

        var cart = await _cartRepository.GetByIdAsync(request.CartId);
        if (cart == null) return NotFound();

        cart.RemoveItem(request.ItemId);

        return Ok();
    }
}
