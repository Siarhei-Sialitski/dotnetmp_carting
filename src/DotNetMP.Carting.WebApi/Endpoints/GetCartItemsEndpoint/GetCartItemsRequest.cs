using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMP.Carting.WebApi.Endpoints.GetCartItemsEndpoint;

public class GetCartItemsRequest
{
    public const string Route = "/Carts/{CartId:guid}/Items";

    [Required]
    [FromRoute]
    public Guid CartId { get; set; }
}
