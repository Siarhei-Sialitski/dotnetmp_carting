using Ardalis.GuardClauses;
using DotNetMP.SharedKernel;
using DotNetMP.SharedKernel.Interfaces;
using NotFoundException = DotNetMP.SharedKernel.Exceptions.NotFoundException;

namespace DotNetMP.Carting.Core.Aggregates.CartAggregate;

public class Cart : EntityBase, IAggregateRoot
{
    private List<Item> _items = new List<Item>();

    public IEnumerable<Item> Items 
    {
        get
        {
            return _items.AsReadOnly();
        }
        private set 
        {
            _items = value.ToList();
        }
    }

    protected Cart()
    { }

    public Cart(Guid id, List<Item> items)
    {
        Id = id;
        _items = items.ToList();
    }

    public void AddItem(Item item)
    {
        Guard.Against.Null(item);

        if (_items.Contains(item))
        {
            var existingItem = _items.Single(i => i.Equals(item));
            existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);

            return;
        }

        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var itemToRemove = _items.FirstOrDefault(i => i.Id == itemId);
        if (itemToRemove == null)
        {
            throw new NotFoundException("Item was not found in cart.");
        }

        _items.Remove(itemToRemove);
    }
}