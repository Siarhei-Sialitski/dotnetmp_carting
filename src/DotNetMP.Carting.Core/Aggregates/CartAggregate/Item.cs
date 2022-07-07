using Ardalis.GuardClauses;
using DotNetMP.SharedKernel;

namespace DotNetMP.Carting.Core.Aggregates.CartAggregate;

public class Item : EntityBase, IEquatable<Item>
{
    public string Name { get; private set; } = null!;
    public Image? Image { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    protected Item()
    { }

    public Item(Guid id, string name, decimal price, int quantity, Image? image = null)
    {
        Id = id;
        Name = Guard.Against.NullOrWhiteSpace(name);
        Image = image;
        Price = Guard.Against.NegativeOrZero(price);
        Quantity = Guard.Against.NegativeOrZero(quantity);
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = Guard.Against.NegativeOrZero(quantity);
    }

    #region IEquatable<Item>

    public bool Equals(Item? other)
    {
        if (other == null) return false;
        return (Id.Equals(other.Id));
    }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        return Equals(obj as Item);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() + Quantity.GetHashCode() + Price.GetHashCode();
    }

    #endregion
}
