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
            .Include(s => s.Images)
            .AsQueryable();

        // Фильтр по категории, если указан
        if (!string.IsNullOrEmpty(request.CategoryFilter) && request.CategoryFilter != "all")
        {
            query = query.Where(s => s.Category != null && s.Category.CategoryName == request.CategoryFilter);
        }

        // Загружаем магазины в память с Images
        var allShops = await query.ToListAsync(cancellationToken);

        // Преобразуем в DTO с расчетом популярности
        var shopDtos = allShops.Select(s => 
        {
            var reviewCount = _context.Reviews.Count(r => r.StoreName == s.ShopName);
            var averageRating = _context.Reviews.Any(r => r.StoreName == s.ShopName)
                ? _context.Reviews
                    .Where(r => r.StoreName == s.ShopName)
                    .Average(r => (r.StoreWork + r.ConsultantWork + r.CashierWork + r.QualityGoods) / 4.0m)
                : (decimal?)null;

            return new ShopCatalogDto
            {
                ShopId = s.ShopId,
                ShopName = s.ShopName,
                ShopCardImage = s.ShopCardImage,
                CategoryName = s.Category != null ? s.Category.CategoryName : null,
                ReviewCount = reviewCount,
                AverageRating = averageRating ?? 0,
                PopularityScore = reviewCount > 0 && averageRating.HasValue 
                    ? averageRating.Value * reviewCount 
                    : 0,
                ImagePaths = s.Images.Select(i => i.ImagePath).ToList()
            };
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
