namespace DotNetMP.Carting.WebApi.Endpoints;

public record ItemRecord(Guid id, string name, decimal price, int quantity, ImageRecord? image);