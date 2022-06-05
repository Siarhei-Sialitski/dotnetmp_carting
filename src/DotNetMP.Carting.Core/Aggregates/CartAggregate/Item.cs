using DotNetMP.SharedKernel;

namespace DotNetMP.Carting.Core.Aggregates.CartAggregate;

public class Item : EntityBase, IEquatable<Item>
{
    public string Name { get; private set; } = null!;
    public Image? Image { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }

    protected Item()
    { }

    public Item(Guid id, string name, double price, int quantity, Image? image)
    {
        Id = id;
        Name = name;
        Image = image;
        Price = price;
        Quantity = quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        Quantity = quantity;
    }

    #region IEquatable<Item>

    public bool Equals(Item? other)
    {
        if (other == null) return false;
        return (Id.Equals(other.Id));
    }

    #endregion
}
