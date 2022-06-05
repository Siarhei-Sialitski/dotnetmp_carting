using DotNetMP.Carting.WebApi.ViewModels;

namespace DotNetMP.Carting.WebApi.Endpoints.GetCartItemsEndpoint;

public class GetCartItemsResponse
{
    public Guid Id { get; set; }
    public IList<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
}
