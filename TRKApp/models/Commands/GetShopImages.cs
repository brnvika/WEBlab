using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TRKApp.Models.Commands
{
    public class GetShopImages : IRequest<List<string>>
    {
        public Guid ShopId { get; set; }
    }

    public class GetShopImagesHandler : IRequestHandler<GetShopImages, List<string>>
    {
        private readonly DataContext _context;

        public GetShopImagesHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetShopImages request, CancellationToken cancellationToken)
        {
            var images = await _context.ShopImages
                .Where(si => si.ShopId == request.ShopId)
                .Select(si => si.ImagePath)
                .ToListAsync(cancellationToken);

            return images;
        }
    }
}
