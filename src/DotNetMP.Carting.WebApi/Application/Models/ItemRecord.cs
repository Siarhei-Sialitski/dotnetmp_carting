namespace DotNetMP.Carting.WebApi.Application.Models;

public record ItemRecord(Guid Id, string Name, decimal Price, int Quantity, ImageRecord? Image);