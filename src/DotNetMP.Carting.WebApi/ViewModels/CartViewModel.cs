namespace DotNetMP.Carting.WebApi.ViewModels;

public class CartViewModel
{
    public Guid Id { get; set; }
    public IList<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
}
