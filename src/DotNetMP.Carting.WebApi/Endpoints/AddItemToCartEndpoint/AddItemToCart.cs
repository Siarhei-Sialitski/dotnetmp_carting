using Ardalis.ApiEndpoints;
using AutoMapper;
using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.Carting.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.AddItemToCartEndpoint;

public class AddItemToCart : EndpointBaseAsync
  .WithRequest<AddItemToCartRequest>
  .WithActionResult<AddItemToCartResponse>
{
    private readonly ICartCommandService _cartCommandService;
    private readonly IMapper _mapper;

    public AddItemToCart(
        ICartCommandService cartCommandService,
        IMapper mapper)
    {
        _cartCommandService = cartCommandService;
        _mapper = mapper;
    }

    [HttpPost(AddItemToCartRequest.Route)]
    public async override Task<ActionResult<AddItemToCartResponse>> HandleAsync(AddItemToCartRequest request, CancellationToken cancellationToken = default)
    {
        var item = _mapper.Map<Item>(request.Item);
        var cart = await _cartCommandService.AddItemToCartAsync(request.CartId, item);

        return _mapper.Map<AddItemToCartResponse>(cart);
    }
}
