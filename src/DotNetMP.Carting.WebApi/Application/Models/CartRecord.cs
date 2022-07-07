namespace DotNetMP.Carting.WebApi.Application.Models;

public record CartRecord(Guid Id, IList<ItemRecord> Items);