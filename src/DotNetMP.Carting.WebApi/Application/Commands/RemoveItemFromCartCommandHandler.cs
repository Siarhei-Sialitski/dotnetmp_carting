using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.SharedKernel.Exceptions;
using DotNetMP.SharedKernel.Interfaces;
using MediatR;

namespace DotNetMP.Carting.WebApi.Application.Commands;

public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand>
{
    private readonly IRepository<Cart> _cartsRepository;

    public RemoveItemFromCartCommandHandler(IRepository<Cart> cartsRepository)
    {
        _cartsRepository = cartsRepository;
    }

    public async Task<Unit> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartsRepository.GetByIdAsync(request.CartId, cancellationToken);
        if (cart == null) throw new NotFoundException("Cart not found.");

        cart.RemoveItem(request.ItemId);
        await _cartsRepository.UpdateAsync(cart, cancellationToken);

        if (!cart.Items.Any())
        {
            await _cartsRepository.DeleteAsync(cart, cancellationToken);
        }

        return Unit.Value;
    }
}
