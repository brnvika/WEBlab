using MediatR;
using Microsoft.EntityFrameworkCore;
using TRKApp.Models;

namespace TRKApp.Models.Commands;

/// <summary>
/// Команда удаления элемента из корзины
/// </summary>
public sealed class RemoveFromCartCommand : IRequest<bool>
{
    public required Guid UserId { get; init; }
    public required Guid ItemId { get; init; }
}

/// <summary>
/// Обработчик команды удаления из корзины
/// </summary>
public sealed class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, bool>
{
    private readonly DataContext _context;

    public RemoveFromCartHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

        if (cart == null)
        {
            return false;
        }

        var item = cart.Items.FirstOrDefault(i => i.Id == request.ItemId);
        if (item == null)
        {
            return false;
        }

        cart.Items.Remove(item);
        cart.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
