using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TRKApp.Commands;

public class GetShopQuery : IRequest<ShopDto?>
{
    public required string ShopName { get; set; }
}

public class GetShopHandler : IRequestHandler<GetShopQuery, ShopDto?>
{
    private readonly DataContext _context;

    public GetShopHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<ShopDto?> Handle(GetShopQuery request, CancellationToken cancellationToken)
    {
        var shop = await _context.Shops
            .Include(s => s.Characteristics)
            .FirstOrDefaultAsync(s => s.ShopName == request.ShopName, cancellationToken);

        if (shop == null)
            return null;

        return new ShopDto
        {
            ShopId = shop.ShopId,
            ShopName = shop.ShopName,
            ShopSait = shop.ShopSait,
            ShopDescription = shop.ShopDescription,
            ShopInformation = shop.ShopInformation,
            ShopLogo = shop.ShopLogo,
            ShopTRK = shop.ShopTRK,
            ShopAdvert = shop.ShopAdvert,
            Characteristics = shop.Characteristics
                .Select(c => new ShopCharacteristicDto
                {
                    Parameter = c.Parameter,
                    Description = c.Description
                })
                .ToList()
        };
    }
}

public class ShopDto
{
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string ShopSait { get; set; } = string.Empty;
    public string ShopDescription { get; set; } = string.Empty;
    public string ShopInformation { get; set; } = string.Empty;
    public string ShopLogo { get; set; } = string.Empty;
    public string ShopTRK { get; set; } = string.Empty;
    public string ShopAdvert { get; set; } = string.Empty;
    public List<ShopCharacteristicDto> Characteristics { get; set; } = new();
}

public class ShopCharacteristicDto
{
    public string Parameter { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
