using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TRKApp.Models.Commands;

/// <summary>
/// Запрос отзывов по магазину
/// </summary>
public sealed class GetReviewsByStoreQuery : IRequest<List<ReviewDto>>
{
    public string StoreName { get; init; } = string.Empty;
}

public sealed class GetReviewsByStoreQueryHandler : IRequestHandler<GetReviewsByStoreQuery, List<ReviewDto>>
{
    private readonly DataContext _dataContext;

    public GetReviewsByStoreQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<ReviewDto>> Handle(GetReviewsByStoreQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _dataContext.Reviews
            .Include(r => r.User)
            .Where(r => r.StoreName == request.StoreName)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewDto
            {
                ReviewId = r.ReviewId,
                UserName = r.User.Name + " " + r.User.Surname,
                StoreWork = r.StoreWork,
                ConsultantWork = r.ConsultantWork,
                CashierWork = r.CashierWork,
                QualityGoods = r.QualityGoods,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return reviews;
    }
}

/// <summary>
/// DTO отзыва для отображения
/// </summary>
public class ReviewDto
{
    public Guid ReviewId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int StoreWork { get; set; }
    public int ConsultantWork { get; set; }
    public int CashierWork { get; set; }
    public int QualityGoods { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
