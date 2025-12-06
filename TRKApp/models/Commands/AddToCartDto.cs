using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands;

/// <summary>
/// DTO для добавления товара в корзину
/// </summary>
public sealed class AddToCartDto
{
    /// <summary>
    /// Идентификатор торговой площади
    /// </summary>
    [Required(ErrorMessage = "Идентификатор площади обязателен")]
    public Guid RentalSpaceId { get; init; }

    /// <summary>
    /// Количество месяцев аренды
    /// </summary>
    [Required]
    [Range(1, 120, ErrorMessage = "Количество месяцев должно быть от 1 до 120")]
    public int Months { get; init; } = 1;
}
