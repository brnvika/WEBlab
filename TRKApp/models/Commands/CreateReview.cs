using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands;

/// <summary>
/// Команда создания отзыва
/// </summary>
public sealed class CreateReviewCommand : IRequest<Guid>
{
    /// <summary>
    /// ID пользователя (из cookies)
    /// </summary>
    [Required]
    public Guid UserId { get; init; }

    /// <summary>
    /// Данные отзыва
    /// </summary>
    [FromBody]
    [Required]
    public required CreateReviewDto Review { get; init; }
}

public sealed class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly DataContext _dataContext;

    public CreateReviewCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = request.UserId,
            StoreName = request.Review.StoreName!,
            StoreWork = request.Review.StoreWork,
            ConsultantWork = request.Review.ConsultantWork,
            CashierWork = request.Review.CashierWork,
            QualityGoods = request.Review.QualityGoods,
            Comment = request.Review.Comment!,
            CreatedAt = DateTime.UtcNow
        };

        await _dataContext.Reviews.AddAsync(review, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return review.ReviewId;
    }
}
