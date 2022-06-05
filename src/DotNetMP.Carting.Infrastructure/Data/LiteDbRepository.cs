using DotNetMP.SharedKernel;
using DotNetMP.SharedKernel.Interfaces;
using LiteDB;

namespace DotNetMP.Carting.Infrastructure.Data;

internal class LiteDbRepository<T> : IRepository<T> where T : EntityBase, IAggregateRoot
{
    private readonly IClientFactory<ILiteDatabase> _liteDatabaseFactory;

    private string CollectionName => typeof(T).Name;

    public LiteDbRepository(IClientFactory<ILiteDatabase> liteDatabaseFactory)
    {
        _liteDatabaseFactory = liteDatabaseFactory;
    }

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var db = GetLiteDatabase();
        var collection = GetCollection(db);

        collection.Insert(entity.Id, entity);
        return Task.FromResult(entity);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var db = GetLiteDatabase();
        var collection = GetCollection(db);

        collection.Delete(entity.Id);

        return Task.CompletedTask;
    }

    public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        using var db = GetLiteDatabase();
        var collection = GetCollection(db);

        var entity = collection.FindById(new BsonValue(id));

        return Task.FromResult(entity ?? null);
    }

    public Task<IList<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        using var db = GetLiteDatabase();
        var collection = GetCollection(db);

        IList<T> entities = collection.FindAll().ToList();

        return Task.FromResult(entities);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var db = GetLiteDatabase();
        var collection = GetCollection(db);

        collection.Update(entity.Id, entity);

        return Task.FromResult(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0);
    }

    private ILiteDatabase GetLiteDatabase()
    {
        return _liteDatabaseFactory.GetClient();
    }

    private ILiteCollection<T> GetCollection(ILiteDatabase liteDatabase)
    {
        return liteDatabase.GetCollection<T>(CollectionName);
    }
}
