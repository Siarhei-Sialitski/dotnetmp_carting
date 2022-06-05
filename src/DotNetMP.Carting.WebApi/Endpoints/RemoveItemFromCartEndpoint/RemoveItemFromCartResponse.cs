using DotNetMP.Carting.WebApi.ViewModels;

namespace DotNetMP.Carting.WebApi.Endpoints.RemoveItemFromCartEndpoint;

public class RemoveItemFromCartResponse
{
    public Guid Id { get; set; }
    public IList<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
}
