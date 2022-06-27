namespace DotNetMP.Carting.WebApi.Application.Models;

public record ItemRecord(Guid id, string name, decimal price, int quantity, ImageRecord? image);