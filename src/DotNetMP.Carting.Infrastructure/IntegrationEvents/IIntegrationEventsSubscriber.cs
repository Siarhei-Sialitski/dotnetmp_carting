namespace DotNetMP.Carting.Infrastructure.IntegrationEvents;

public interface IIntegrationEventsSubscriber
{
    Task Subscribe<T>(string queueName);
}
