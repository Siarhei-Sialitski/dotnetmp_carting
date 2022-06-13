using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.AddItemToCartEndpoint;

public class AddItemToCartRequest
{
    public const string Route = "/Carts/{CartId:guid}/Items";

    [Required]
    [FromRoute]
    public Guid CartId { get; set; }

    [FromBody]
    public ItemDto Item { get; set; } = null!;
}
