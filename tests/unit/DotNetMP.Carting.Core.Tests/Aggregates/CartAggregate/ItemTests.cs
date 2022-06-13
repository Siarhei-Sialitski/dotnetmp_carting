using DotNetMP.Carting.Core.Aggregates.CartAggregate;

namespace DotNetMP.Carting.Core.Tests.Aggregates.CartAggregate;

public class ItemTests
{

    [Fact]
    public void Constructor_NullNameIsSent_ArgumentNullExceptionThrown()
    {
        // Arrange
        string nullName = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new Item(Guid.NewGuid(), nullName, 1, 1));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("name", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_InvalidNameIsSent_ArgumentExceptionThrown(string name)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Item(Guid.NewGuid(), name, 1, 1));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("name", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Constructor_InvalidPriceIsSent_ArgumentExceptionThrown(decimal price)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Item(Guid.NewGuid(), "name", price, 1));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("price", exception.ParamName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Constructor_InvalidQuantityIsSent_ArgumentExceptionThrown(int quantity)
    {
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new Item(Guid.NewGuid(), "name", 1, quantity));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("quantity", exception.ParamName);
    }

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
        var exception = Assert.Throws<ArgumentException>(() => item.UpdateQuantity(quantity));

        // Assert
        Assert.NotNull(exception);
        Assert.Equal("quantity", exception.ParamName);
    }
}
