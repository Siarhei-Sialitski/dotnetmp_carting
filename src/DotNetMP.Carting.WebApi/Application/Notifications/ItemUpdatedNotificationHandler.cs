using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.SharedKernel.Interfaces;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Notifications;

public class ItemUpdatedNotificationHandler : INotificationHandler<ItemUpdatedNotification>
{
    private readonly IRepository<Cart> _cartsRepository;

    public ItemUpdatedNotificationHandler(IRepository<Cart> cartsRepository)
    {
        _cartsRepository = cartsRepository;
    }

    public async Task Handle(ItemUpdatedNotification notification, CancellationToken cancellationToken)
    {
        var updatedItem = notification.ItemUpdatedIntegrationEvent;
        var carts = await _cartsRepository.ListAsync(cancellationToken);
        foreach (var cart in carts)
        {
            var item = cart.Items.Where(i => i.Id == updatedItem.Id).SingleOrDefault();
            if (item != null)
            {
                item.UpdateName(updatedItem.Name);
                item.UpdatePrice(updatedItem.Price);

                if (!string.IsNullOrWhiteSpace(updatedItem.Image))
                {
                    item.UpdateImage(new Image(updatedItem.Image, string.Empty));
                }
                else
                {
                    item.UpdateImage(null);
                }

                await _cartsRepository.UpdateAsync(cart, cancellationToken);
            }
        }
    }
}
