using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRKApp.Models;

/// <summary>
/// Элемент корзины
/// </summary>
public class CartItem
{
    /// <summary>
    /// Уникальный идентификатор элемента корзины
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор корзины
    /// </summary>
    [Required]
    public Guid CartId { get; set; }

    /// <summary>
    /// Навигационное свойство к корзине
    /// </summary>
    [ForeignKey(nameof(CartId))]
    public virtual Cart Cart { get; set; } = null!;

    /// <summary>
    /// Идентификатор торговой площади
    /// </summary>
    [Required]
    public Guid RentalSpaceId { get; set; }

    /// <summary>
    /// Навигационное свойство к торговой площади
    /// </summary>
    [ForeignKey(nameof(RentalSpaceId))]
    public virtual RentalSpace RentalSpace { get; set; } = null!;

    /// <summary>
    /// Количество месяцев аренды
    /// </summary>
    [Required]
    [Range(1, 120)]
    public int Months { get; set; } = 1;

    /// <summary>
    /// Дата добавления в корзину
    /// </summary>
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
