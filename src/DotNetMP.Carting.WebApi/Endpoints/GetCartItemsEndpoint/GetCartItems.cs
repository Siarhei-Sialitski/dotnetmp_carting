using Ardalis.ApiEndpoints;
using AutoMapper;
using DotNetMP.Carting.Core.Interfaces;
using DotNetMP.Carting.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.GetCartItemsEndpoint;

public class GetCartItems : EndpointBaseAsync
  .WithRequest<GetCartItemsRequest>
  .WithActionResult<GetCartItemsResponse>
{
    private readonly ICartQueryService _cartQueryService;
    private readonly IMapper _mapper;

    public GetCartItems(
        ICartQueryService cartQueryService,
        IMapper mapper)
    {
        _cartQueryService = cartQueryService;
        _mapper = mapper;
    }

    [HttpGet(GetCartItemsRequest.Route)]
    public async override Task<ActionResult<GetCartItemsResponse>> HandleAsync(GetCartItemsRequest request, CancellationToken cancellationToken = default)
    {
        var items = await _cartQueryService.GetCartItemsAsync(request.CartId);
        var itemsViewModel = _mapper.Map<IEnumerable<ItemViewModel>>(items);

        return new GetCartItemsResponse
        {
            Id = request.CartId,
            Items = itemsViewModel.ToList()
        };
    }
}
