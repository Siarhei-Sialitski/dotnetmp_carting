using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.SharedKernel.Exceptions;

namespace DotNetMP.Carting.Core.Tests.Aggregates.CartAggregate;

public class CartTests
{

    [Fact]
    public void AddItem_ItemWasNotAlreadyAdded_ItemAdded()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var item = new Item(itemId, "item", 3, 100, null);
        var cart = new Cart(Guid.NewGuid());

        // Act
        cart.AddItem(item);

        // Assert
        Assert.Contains(item, cart.Items);
    }

    [Fact]
    public void AddItem_ItemWasAlreadyAdded_ItemQuantityAdded()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var quantity = 3;
        var expectedQuantity = quantity * 2;
        var item = new Item(itemId, "item", 10.5M, quantity, null);
        var cart = new Cart(Guid.NewGuid());

        // Act
        cart.AddItem(item);
        cart.AddItem(item);

        // Assert
        Assert.Equal(expectedQuantity, cart.Items.First(i => i.Id == itemId).Quantity);
    }

    [Fact]
    public void RemoveItem_ItemWasAlreadyAdded_ItemRemoved()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var item = new Item(itemId, "item", 3, 100, null);
        var cart = new Cart(Guid.NewGuid());
        cart.AddItem(item);

        // Act
        cart.RemoveItem(itemId);

        // Assert
        Assert.DoesNotContain(item, cart.Items);
    }

    [Fact]
    public void RemoveItem_ItemWasNotAlreadyAdded_NotFoundException()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var cart = new Cart(Guid.NewGuid());

        // Act
        var exception = Assert.Throws<NotFoundException>(() => cart.RemoveItem(itemId));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("Item was not found in cart.", exception.Message);
    }
}
