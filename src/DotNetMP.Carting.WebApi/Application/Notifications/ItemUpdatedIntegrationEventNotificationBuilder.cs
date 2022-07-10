using DotNetMP.Carting.Infrastructure.IntegrationEvents;
using DotNetMP.SharedKernel.IntegrationEvents;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Notifications;

public class ItemUpdatedIntegrationEventNotificationBuilder : IIntegrationEventNotificationBuilder<ItemUpdatedIntegrationEvent>
{
    public INotification Build(ItemUpdatedIntegrationEvent t)
    {
        return new ItemUpdatedNotification(t);
    }
}
