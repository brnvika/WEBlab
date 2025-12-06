using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRKApp.Models;

/// <summary>
/// Торговая площадь для аренды
/// </summary>
public class RentalSpace
{
    /// <summary>
    /// Уникальный идентификатор площади
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Название торговой площади
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание площади
    /// </summary>
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Площадь в квадратных метрах
    /// </summary>
    [Required]
    public decimal SquareMeters { get; set; }

    /// <summary>
    /// Цена аренды в месяц
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PricePerMonth { get; set; }

    /// <summary>
    /// Этаж
    /// </summary>
    [Required]
    public int Floor { get; set; }

    /// <summary>
    /// Номер площади/секции
    /// </summary>
    [MaxLength(50)]
    public string SpaceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Изображение площади
    /// </summary>
    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Доступна ли площадь для аренды
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Категория площади (бутик, островок, павильон и т.д.)
    /// </summary>
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор магазина, арендующего площадь (если арендована)
    /// </summary>
    public Guid? ShopId { get; set; }

    /// <summary>
    /// Навигационное свойство к магазину
    /// </summary>
    [ForeignKey(nameof(ShopId))]
    public virtual Shop? Shop { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
