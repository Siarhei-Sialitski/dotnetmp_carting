using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.Carting.Core.Services;
using DotNetMP.SharedKernel.Interfaces;
using Moq;

namespace DotNetMP.Carting.Core.Tests.Services;

public class CartQueryServiceTests
{
    private Mock<IRepository<Cart>> _repositoryMock;

    private CartQueryService _queryService;

    public CartQueryServiceTests()
    {
        _repositoryMock = new Mock<IRepository<Cart>>();

        _queryService = new CartQueryService(_repositoryMock.Object);
    }

    [Fact]
    public void Constructor_NullCartRepositoryInjected_ArgumentNullExceptionThrown()
    {
        // Arrange
        IRepository<Cart> nullRepository = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new CartQueryService(nullRepository));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("cartRepository", exception.ParamName);
    }

    [Fact]
    public async Task GetCartItemsAsync_CartNotFound_CartCreateWithoutItems()
    {
        // Arrange
        var id = Guid.NewGuid();
        Cart nullCart = null;
        Cart createdCart = new Cart(id, new List<Item>());
        _repositoryMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(nullCart);
        _repositoryMock
            .Setup(r => r.AddAsync(It.Is<Cart>(c => c.Id == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCart)
            .Verifiable();

        // Act
        var items = await _queryService.GetCartItemsAsync(id);

        // Assert
        Assert.NotNull(items);
        Assert.Empty(items);
        _repositoryMock.VerifyAll();
    }
}
