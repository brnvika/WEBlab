using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands;

/// <summary>
/// DTO для создания отзыва
/// </summary>
public sealed class CreateReviewDto
{
    /// <summary>
    /// Название магазина
    /// </summary>
    [Required(ErrorMessage = "Название магазина обязательно")]
    public string? StoreName { get; init; }

    /// <summary>
    /// Оценка работы магазина (1-5)
    /// </summary>
    [Required]
    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    public int StoreWork { get; init; }

    /// <summary>
    /// Оценка работы консультанта (1-5)
    /// </summary>
    [Required]
    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    public int ConsultantWork { get; init; }

    /// <summary>
    /// Оценка работы кассира (1-5)
    /// </summary>
    [Required]
    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    public int CashierWork { get; init; }

    /// <summary>
    /// Оценка качества товаров (1-5)
    /// </summary>
    [Required]
    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    public int QualityGoods { get; init; }

    /// <summary>
    /// Комментарий
    /// </summary>
    [Required(ErrorMessage = "Комментарий обязателен")]
    [MinLength(10, ErrorMessage = "Минимальная длина комментария - 10 символов")]
    [MaxLength(500, ErrorMessage = "Максимальная длина комментария - 500 символов")]
    public string? Comment { get; init; }
}
