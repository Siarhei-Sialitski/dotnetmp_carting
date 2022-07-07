using DotNetMP.SharedKernel;
using DotNetMP.SharedKernel.Interfaces;
using LiteDB;
using Moq;

namespace DotNetMP.Carting.Infrastructure.Data;

public class LiteDbRepositoryTests
{
    private readonly LiteDbRepository<TestEntity> _liteDbRepository;
    private readonly Mock<IClientFactory<ILiteDatabase>> _liteDbFactoryMock;
    private readonly Mock<ILiteDatabase> _liteDbMock;
    private readonly Mock<ILiteCollection<TestEntity>> _liteCollectionMock;

    public LiteDbRepositoryTests()
    {

        _liteDbFactoryMock = new Mock<IClientFactory<ILiteDatabase>>();
        _liteDbMock = new Mock<ILiteDatabase>();
        _liteCollectionMock = new Mock<ILiteCollection<TestEntity>>();
        _liteDbMock
            .Setup(db => db.GetCollection<TestEntity>(typeof(TestEntity).Name, It.IsAny<BsonAutoId>()))
            .Returns(_liteCollectionMock.Object);
        _liteDbFactoryMock.Setup(o => o.GetClient()).Returns(_liteDbMock.Object);

        _liteDbRepository = new LiteDbRepository<TestEntity>(_liteDbFactoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_EntitySent_ColletionInsertInvoked()
    {
        // Arrange
        var entity = new TestEntity();
        _liteCollectionMock
            .Setup(c => c.Insert(entity.Id, entity))
            .Verifiable();

        // Act
        await _liteDbRepository.AddAsync(entity);

        // Assert
        _liteCollectionMock.VerifyAll();
    }

    [Fact]
    public async Task DeleteAsync_EntitySent_ColletionDeleteInvoked()
    {
        // Arrange
        var entity = new TestEntity();
        _liteCollectionMock
            .Setup(c => c.Delete(entity.Id))
            .Verifiable();

        // Act
        await _liteDbRepository.DeleteAsync(entity);

        // Assert
        _liteCollectionMock.VerifyAll();
    }

    [Fact]
    public async Task GetByIdAsync_EntitySent_ColletionFindByIdInvoked()
    {
        // Arrange
        var entity = new TestEntity();
        _liteCollectionMock
            .Setup(c => c.FindById(entity.Id))
            .Verifiable();

        // Act
        await _liteDbRepository.GetByIdAsync(entity.Id);

        // Assert
        _liteCollectionMock.VerifyAll();
    }

    [Fact]
    public async Task ListAsync_ColletionFindBAllInvoked()
    {
        // Arrange
        _liteCollectionMock
            .Setup(c => c.FindAll())
            .Verifiable();

        // Act
        await _liteDbRepository.ListAsync();

        // Assert
        _liteCollectionMock.VerifyAll();
    }

    [Fact]
    public async Task UpdateAsync_EntitySent_ColletionFindBAllInvoked()
    {
        // Arrange
        var entity = new TestEntity();
        _liteCollectionMock
            .Setup(c => c.Update(entity.Id, entity))
            .Verifiable();

        // Act
        await _liteDbRepository.UpdateAsync(entity);

        // Assert
        _liteCollectionMock.VerifyAll();
    }

    [Fact]
    public async Task SaveChangesAsync_ReturnsCompletedTask()
    {
        // Arrange
        _ = new TestEntity();

        // Act
        var result = await _liteDbRepository.SaveChangesAsync();

        // Assert
        Assert.Equal(0, result);
    }

    public class TestEntity : EntityBase, IAggregateRoot
    { }
}

