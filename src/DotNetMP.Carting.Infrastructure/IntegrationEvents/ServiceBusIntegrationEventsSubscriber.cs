using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetMP.Carting.Infrastructure.IntegrationEvents;

internal class ServiceBusIntegrationEventsSubscriber : IIntegrationEventsSubscriber
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly IMediator _mediator;
    private readonly IServiceProvider _serviceProvider;

    public ServiceBusIntegrationEventsSubscriber(
        ServiceBusClient serviceBusClient,
        IMediator mediator,
        IServiceProvider serviceProvider)
    {
        _serviceBusClient = serviceBusClient;
        _mediator = mediator;
        _serviceProvider = serviceProvider;
    }

    public async Task Subscribe<T>(string queueName)
    {
        var processorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 1,
            AutoCompleteMessages = false,
        };
        var processor = _serviceBusClient.CreateProcessor(queueName, processorOptions);

        var creator = _serviceProvider.GetService<IIntegrationEventNotificationBuilder<T>>();
        if (creator == null)
        {
            throw new Exception($"Notification creator for {typeof(T).Name} not found.");
        }

        processor.ProcessMessageAsync += async (args) =>
        {
            var payload = args.Message.Body.ToObjectFromJson<T>();
            var notification = creator.Build(payload);
            await _mediator.Publish(notification);
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        };

        processor.ProcessErrorAsync += arg =>
            throw new Exception($"Event {arg.FullyQualifiedNamespace} was not processed.", arg.Exception);

        await processor.StartProcessingAsync().ConfigureAwait(false);
    }
}
