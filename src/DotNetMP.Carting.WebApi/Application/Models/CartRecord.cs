namespace DotNetMP.Carting.WebApi.Application.Models;

public record CartRecord(Guid id, IList<ItemRecord> items);