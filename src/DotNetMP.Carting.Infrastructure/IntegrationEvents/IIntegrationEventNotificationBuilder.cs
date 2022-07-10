using MediatR;

namespace DotNetMP.Carting.Infrastructure.IntegrationEvents;

public interface IIntegrationEventNotificationBuilder<T>
{
    INotification Build(T t);
}