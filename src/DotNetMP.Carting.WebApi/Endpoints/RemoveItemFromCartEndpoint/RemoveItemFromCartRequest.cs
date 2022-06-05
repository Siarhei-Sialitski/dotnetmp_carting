using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.RemoveItemFromCartEndpoint;

public class RemoveItemFromCartRequest
{
    public const string Route = "/Carts/{CartId:guid}/Items/{ItemId:guid}";

    [Required]
    [FromRoute]
    public Guid CartId { get; set; }

    [Required]
    [FromRoute]
    public Guid ItemId { get; set; }
}
