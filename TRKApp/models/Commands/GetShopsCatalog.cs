using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetShopsCatalog : IRequest<List<ShopCatalogDto>>
{
    public string? CategoryFilter { get; set; }
    public string? SortBy { get; set; } // "popularity" или "alphabet"
}

public class GetShopsCatalogHandler : IRequestHandler<GetShopsCatalog, List<ShopCatalogDto>>
{
    private readonly DataContext _context;

    public GetShopsCatalogHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ShopCatalogDto>> Handle(GetShopsCatalog request, CancellationToken cancellationToken)
    {
        var query = _context.Shops
            .Include(s => s.Category)
            .AsQueryable();

        // Фильтр по категории, если указан
        if (!string.IsNullOrEmpty(request.CategoryFilter) && request.CategoryFilter != "all")
        {
            query = query.Where(s => s.Category != null && s.Category.CategoryName == request.CategoryFilter);
        }

        // Получаем магазины с расчетом популярности
        var shops = await query
            .Select(s => new
            {
                Shop = s,
                ReviewCount = _context.Reviews.Count(r => r.StoreName == s.ShopName),
                AverageRating = _context.Reviews.Any(r => r.StoreName == s.ShopName)
                    ? _context.Reviews
                        .Where(r => r.StoreName == s.ShopName)
                        .Average(r => (r.StoreWork + r.ConsultantWork + r.CashierWork + r.QualityGoods) / 4.0m)
                    : (decimal?)null
            })
            .ToListAsync(cancellationToken);

        // Преобразуем в DTO с расчетом PopularityScore
        var shopDtos = shops.Select(s => new ShopCatalogDto
        {
            ShopId = s.Shop.ShopId,
            ShopName = s.Shop.ShopName,
            ShopCardImage = s.Shop.ShopCardImage,
            CategoryName = s.Shop.Category != null ? s.Shop.Category.CategoryName : null,
            ReviewCount = s.ReviewCount,
            AverageRating = s.AverageRating ?? 0,
            PopularityScore = s.ReviewCount > 0 && s.AverageRating.HasValue 
                ? s.AverageRating.Value * s.ReviewCount 
                : 0
        }).ToList();

        // Сортировка
        if (request.SortBy == "popularity")
        {
            shopDtos = shopDtos.OrderByDescending(s => s.PopularityScore).ThenBy(s => s.ShopName).ToList();
        }
        else // По умолчанию или "alphabet"
        {
            shopDtos = shopDtos.OrderBy(s => s.ShopName).ToList();
        }

        return shopDtos;
    }
}
