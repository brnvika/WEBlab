using MediatR;
using Microsoft.EntityFrameworkCore;
using TRKApp.Models;

namespace TRKApp.Models.Commands;

/// <summary>
/// Запрос получения корзины пользователя
/// </summary>
public sealed class GetCartQuery : IRequest<CartViewModel?>
{
    public required Guid UserId { get; init; }
}

/// <summary>
/// Модель представления корзины
/// </summary>
public sealed class CartViewModel
{
    public Guid CartId { get; init; }
    public List<CartItemViewModel> Items { get; init; } = new();
    public decimal TotalPrice { get; init; }
}

/// <summary>
/// Модель представления элемента корзины
/// </summary>
public sealed class CartItemViewModel
{
    public Guid ItemId { get; init; }
    public Guid RentalSpaceId { get; init; }
    public string SpaceName { get; init; } = string.Empty;
    public string SpaceNumber { get; init; } = string.Empty;
    public decimal SquareMeters { get; init; }
    public int Floor { get; init; }
    public decimal PricePerMonth { get; init; }
    public int Months { get; init; }
    public decimal Subtotal { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public Guid? ShopId { get; init; }
    public string? ShopName { get; init; }
}

/// <summary>
/// Обработчик запроса корзины
/// </summary>
public sealed class GetCartHandler : IRequestHandler<GetCartQuery, CartViewModel?>
{
    private readonly DataContext _context;

    public GetCartHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<CartViewModel?> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.RentalSpace)
            .ThenInclude(s => s.Shop)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

        if (cart == null || !cart.Items.Any())
        {
            return null;
        }

        var items = cart.Items.Select(item => new CartItemViewModel
        {
            ItemId = item.Id,
            RentalSpaceId = item.RentalSpaceId,
            SpaceName = item.RentalSpace.Name,
            SpaceNumber = item.RentalSpace.SpaceNumber,
            SquareMeters = item.RentalSpace.SquareMeters,
            Floor = item.RentalSpace.Floor,
            PricePerMonth = item.RentalSpace.PricePerMonth,
            Months = item.Months,
            Subtotal = item.RentalSpace.PricePerMonth * item.Months,
            ImageUrl = item.RentalSpace.ImageUrl,
            Category = item.RentalSpace.Category,
            ShopId = item.RentalSpace.ShopId,
            ShopName = item.RentalSpace.Shop?.ShopName
        }).ToList();

        return new CartViewModel
        {
            CartId = cart.Id,
            Items = items,
            TotalPrice = items.Sum(i => i.Subtotal)
        };
    }
}
