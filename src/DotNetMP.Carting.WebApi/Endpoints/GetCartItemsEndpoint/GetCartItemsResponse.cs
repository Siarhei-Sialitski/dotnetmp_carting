using DotNetMP.Carting.WebApi.Endpoints.AddItemToCartEndpoint;

namespace DotNetMP.Carting.WebApi.Endpoints.GetCartItemsEndpoint;

public class GetCartItemsResponse
{
    public IList<ItemRecord> Items { get; set; } = new List<ItemRecord>();
}
