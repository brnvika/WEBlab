using MediatR;
using Microsoft.EntityFrameworkCore;
using TRKApp.Models;

namespace TRKApp.Models.Commands;

/// <summary>
/// Запрос получения списка торговых площадей
/// </summary>
public sealed class GetRentalSpacesQuery : IRequest<List<RentalSpaceViewModel>>
{
    public string? Category { get; init; }
    public bool? AvailableOnly { get; init; } = true;
}

/// <summary>
/// Модель представления торговой площади
/// </summary>
public sealed class RentalSpaceViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal SquareMeters { get; init; }
    public decimal PricePerMonth { get; init; }
    public int Floor { get; init; }
    public string SpaceNumber { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public bool IsAvailable { get; init; }
    public string Category { get; init; } = string.Empty;
    public Guid? ShopId { get; init; }
    public string? ShopName { get; init; }
}

/// <summary>
/// Обработчик запроса списка торговых площадей
/// </summary>
public sealed class GetRentalSpacesHandler : IRequestHandler<GetRentalSpacesQuery, List<RentalSpaceViewModel>>
{
    private readonly DataContext _context;

    public GetRentalSpacesHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<RentalSpaceViewModel>> Handle(GetRentalSpacesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.RentalSpaces.AsQueryable();

        if (request.AvailableOnly == true)
        {
            query = query.Where(s => s.IsAvailable);
        }

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(s => s.Category == request.Category);
        }

        var spaces = await query
            .Include(s => s.Shop)
            .OrderBy(s => s.Floor)
            .ThenBy(s => s.SpaceNumber)
            .ToListAsync(cancellationToken);

        return spaces.Select(s => new RentalSpaceViewModel
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            SquareMeters = s.SquareMeters,
            PricePerMonth = s.PricePerMonth,
            Floor = s.Floor,
            SpaceNumber = s.SpaceNumber,
            ImageUrl = s.ImageUrl,
            IsAvailable = s.IsAvailable,
            Category = s.Category,
            ShopId = s.ShopId,
            ShopName = s.Shop?.ShopName
        }).ToList();
    }
}
