using DotNetMP.SharedKernel;
using DotNetMP.SharedKernel.Exceptions;
using DotNetMP.SharedKernel.Interfaces;

namespace DotNetMP.Carting.Core.Aggregates.CartAggregate;

public class Cart : EntityBase, IAggregateRoot
{

    public IList<Item> Items { get; set; } = new List<Item>();

    protected Cart()
    { }

    public Cart(Guid id, List<Item> items)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        Id = id;
        Items = items.ToList();
    }

    public void AddItem(Item item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        if (Items.Contains(item))
        {
            var existingItem = Items.Single(i => i.Equals(item));
            existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);

            return;
        }

        Items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var itemToRemove = Items.FirstOrDefault(i => i.Id == itemId);
        if (itemToRemove == null)
        {
            throw new NotFoundException("Item was not found in cart.");
        }

        Items.Remove(itemToRemove);
    }
}