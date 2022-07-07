using System.Net;
using DotNetMP.Carting.WebApi.Application.Commands;
using DotNetMP.Carting.WebApi.Application.Models;
using DotNetMP.Carting.WebApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class CartsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(CartRecord), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CartRecord>> GetCartV1(Guid id, CancellationToken cancellationToken)
    {
        var commandResult = await _mediator.Send(new GetCartQueryV1(id), cancellationToken);

        return Ok(commandResult);
    }

    [HttpGet("{id}")]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(typeof(IList<ItemRecord>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<IList<ItemRecord>>> GetCartV2(Guid id, CancellationToken cancellationToken)
    {
        var commandResult = await _mediator.Send(new GetCartQueryV2(id), cancellationToken);

        return Ok(commandResult);
    }

    [HttpPost("{id}/items")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddItemToCart(Guid id, [FromBody] AddItemDto item, CancellationToken cancellationToken)
    {
        await _mediator
            .Send(new AddItemToCartCommand(id, item.Id, item.Name, item.Price, item.Quantity, item.ImageUrl, item.ImageAltText)
            , cancellationToken);

        return NoContent();
    }

    [HttpDelete("{cartId}/items/{itemId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid itemId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveItemFromCartCommand(cartId, itemId), cancellationToken);

        return NoContent();
    }
}
