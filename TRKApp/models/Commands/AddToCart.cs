using MediatR;
using Microsoft.EntityFrameworkCore;
using TRKApp.Models;

namespace TRKApp.Models.Commands;

/// <summary>
/// Команда добавления товара в корзину
/// </summary>
public sealed class AddToCartCommand : IRequest<Guid>
{
    public required Guid UserId { get; init; }
    public required AddToCartDto Item { get; init; }
}

/// <summary>
/// Обработчик команды добавления в корзину
/// </summary>
public sealed class AddToCartHandler : IRequestHandler<AddToCartCommand, Guid>
{
    private readonly DataContext _context;

    public AddToCartHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        // Получаем или создаём корзину пользователя
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

        if (cart == null)
        {
            cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Carts.Add(cart);
        }

        // Проверяем, есть ли уже такая площадь в корзине
        var existingItem = cart.Items.FirstOrDefault(i => i.RentalSpaceId == request.Item.RentalSpaceId);
        
        if (existingItem != null)
        {
            // Если уже есть, обновляем количество месяцев
            existingItem.Months = request.Item.Months;
        }
        else
        {
            // Добавляем новый элемент
            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                RentalSpaceId = request.Item.RentalSpaceId,
                Months = request.Item.Months,
                AddedAt = DateTime.UtcNow
            };
            cart.Items.Add(cartItem);
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return cart.Id;
    }
}
