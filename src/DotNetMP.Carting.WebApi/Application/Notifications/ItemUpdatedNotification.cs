using DotNetMP.SharedKernel.IntegrationEvents;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Notifications;

public record ItemUpdatedNotification(ItemUpdatedIntegrationEvent ItemUpdatedIntegrationEvent) : INotification;
