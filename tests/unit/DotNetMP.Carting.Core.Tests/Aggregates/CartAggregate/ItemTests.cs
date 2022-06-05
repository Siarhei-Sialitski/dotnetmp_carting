using DotNetMP.Carting.Core.Aggregates.CartAggregate;

namespace DotNetMP.Carting.Core.Tests.Aggregates.CartAggregate;

public class ItemTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(500)]
    public void UpdateQuantity_ValidQuantitySent_QuantityUpdated(int quantity)
    {
        // Arrange
        var item = new Item(Guid.NewGuid(), "item", 100, 1, null);

        // Act
        item.UpdateQuantity(quantity);

        // Assert
        Assert.Equal(quantity, item.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdateQuantity_InvalidQuantitySent_ArgumentOutOfRangeExceptionThrown(int quantity)
    {
        // Arrange
        var item = new Item(Guid.NewGuid(), "item", 100, 5, null);

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => item.UpdateQuantity(quantity));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("quantity", exception.ParamName);
    }
}
