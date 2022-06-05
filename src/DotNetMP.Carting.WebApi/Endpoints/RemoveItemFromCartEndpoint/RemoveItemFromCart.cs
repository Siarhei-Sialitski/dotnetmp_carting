using Ardalis.ApiEndpoints;
using AutoMapper;
using DotNetMP.Carting.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.RemoveItemFromCartEndpoint;

public class RemoveItemFromCart : EndpointBaseAsync
  .WithRequest<RemoveItemFromCartRequest>
  .WithActionResult<RemoveItemFromCartResponse>
{
    private readonly ICartCommandService _cartCommandService;
    private readonly IMapper _mapper;

    public RemoveItemFromCart(
        ICartCommandService cartCommandService,
        IMapper mapper)
    {
        _cartCommandService = cartCommandService;
        _mapper = mapper;
    }

    [HttpDelete(RemoveItemFromCartRequest.Route)]
    public async override Task<ActionResult<RemoveItemFromCartResponse>> HandleAsync(RemoveItemFromCartRequest request, CancellationToken cancellationToken = default)
    {
        var cart = await _cartCommandService.RemoveItemFromCartAsync(request.CartId, request.ItemId);

        return _mapper.Map<RemoveItemFromCartResponse>(cart);
    }
}
