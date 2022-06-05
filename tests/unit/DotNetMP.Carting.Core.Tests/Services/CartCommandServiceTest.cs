using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.Carting.Core.Services;
using DotNetMP.SharedKernel.Exceptions;
using DotNetMP.SharedKernel.Interfaces;
using Moq;

namespace DotNetMP.Carting.Core.Tests.Services;

public class CartCommandServiceTests
{
    private Mock<IRepository<Cart>> _repositoryMock;

    private CartCommandService _commandService;

    public CartCommandServiceTests()
    {
        _repositoryMock = new Mock<IRepository<Cart>>();

        _commandService = new CartCommandService(_repositoryMock.Object);
    }

    [Fact]
    public void Constructor_NullCartRepositoryInjected_ArgumentNullExceptionThrown()
    {
        // Arrange
        IRepository<Cart> nullRepository = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new CartCommandService(nullRepository));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("cartRepository", exception.ParamName);
    }

    [Fact]
    public async Task AddItemToCartAsync_CartNotFound_NewCartCreated()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        Cart nullCart = null;
        Cart createdCart = new Cart(cartId, new List<Item>());
        var item = new Item(itemId, "item", 3, 100, null);

        _repositoryMock
            .Setup(r => r.GetByIdAsync(cartId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(nullCart);
        _repositoryMock
            .Setup(r => r.AddAsync(It.Is<Cart>(c => c.Id == cartId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCart)
            .Verifiable();

        // Act
        var cart = await _commandService.AddItemToCartAsync(cartId, item);

        // Assert
        Assert.NotNull(cart);
        Assert.Equal(cartId, cart.Id);
        Assert.Contains(item, cart.Items);
    }

    [Fact]
    public async Task AddItemToCartAsync_CartFound_ReturnsCartWithAddedItem()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var itemToAdd = new Item(itemId, "item", 3, 100, null);
        Cart cart = new Cart(cartId, new List<Item>());
        _repositoryMock
            .Setup(r => r.GetByIdAsync(cartId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        // Act
        var returnedCart = await _commandService.AddItemToCartAsync(cartId, itemToAdd);

        // Assert
        Assert.NotNull(returnedCart);
        Assert.Equal(cartId, returnedCart.Id);
    }

    [Fact]
    public async Task RemoveItemFromCartAsync_CartNotFound_NotFoundEceptionThrown()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        Cart nullCart = null;
        _repositoryMock
            .Setup(r => r.GetByIdAsync(cartId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(nullCart);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _commandService.RemoveItemFromCartAsync(cartId, itemId));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("Cart was not found.", exception.Message);
    }

    [Fact]
    public async Task RemoveItemFromCartAsync_ItemNotFound_NotFoundEceptionThrown()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        Cart cart = new Cart(itemId, new List<Item>());
        _repositoryMock
            .Setup(r => r.GetByIdAsync(cartId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _commandService.RemoveItemFromCartAsync(cartId, itemId));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("Item was not found in cart.", exception.Message);
    }

    [Fact]
    public async Task RemoveItemFromCartAsync_ItemFound_ReturnsCartWithoutRemovedItem()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var itemToRemove = new Item(itemId, "item", 3, 100, null);
        Cart cart = new Cart(cartId, new List<Item>()
        {
            itemToRemove,
            new Item(Guid.NewGuid(), "another item", 5, 200, null)
        });
        _repositoryMock
            .Setup(r => r.GetByIdAsync(cartId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        // Act
        var returnedCart = await  _commandService.RemoveItemFromCartAsync(cartId, itemId);

        // Assert
        Assert.NotNull(returnedCart);
        Assert.Equal(cartId, returnedCart.Id);
    }
}
