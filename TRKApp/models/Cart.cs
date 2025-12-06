using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRKApp.Models;

/// <summary>
/// Корзина пользователя
/// </summary>
public class Cart
{
    /// <summary>
    /// Уникальный идентификатор корзины
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Навигационное свойство к пользователю
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// Элементы в корзине
    /// </summary>
    public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    /// <summary>
    /// Дата создания корзины
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
