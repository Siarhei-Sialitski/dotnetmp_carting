using Ardalis.ApiEndpoints;
using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.AddItemToCartEndpoint;

public class AddItemToCart : EndpointBaseAsync
  .WithRequest<AddItemToCartRequest>
  .WithActionResult
{
    private readonly IRepository<Cart> _cartRepository;

    public AddItemToCart(IRepository<Cart> cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpPost(AddItemToCartRequest.Route)]
    public async override Task<ActionResult> HandleAsync(AddItemToCartRequest request, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByIdAsync(request.CartId);
        if (cart == null)
        {
            cart = new Cart(request.CartId);
            await _cartRepository.AddAsync(cart);
        }

        Image? image = null;
        if (request.Item.Image != null)
        {
            image = new Image(request.Item.Image.Url, request.Item.Image.AltText);
        }

        var item = new Item(request.Item.Id, request.Item.Name, request.Item.Price, request.Item.Quantity, image);
        cart.AddItem(item);
        await _cartRepository.UpdateAsync(cart);

        return Ok();
    }
}
