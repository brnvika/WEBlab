using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Shop
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid ShopId { get; set; }
    
    [Required, Length(2, 25)]
    public string ShopName { get; set; } = string.Empty;

    [Required]
    public string ShopSait { get; set; } = string.Empty;

    [Required, Length(20, 300)]
    public string ShopDescription { get; set; } = string.Empty;

    [Required, Length(80, 600)]
    public string ShopInformation { get; set; } = string.Empty;

    [Required]
    public string ShopLogo { get; set; } = string.Empty;

    [Required]
    public string ShopTRK { get; set; } = string.Empty;

    [Required]
    public string ShopAdvert { get; set; } = string.Empty;

    [Required]
    public string ShopCardImage { get; set; } = string.Empty;

    // Внешний ключ для категории (nullable, так как у существующих магазинов еще нет категории)
    public Guid? CategoryId { get; set; }

    // Навигационные свойства
    public Category? Category { get; set; }
    public ICollection<ShopCharacteristic> Characteristics { get; set; } = new List<ShopCharacteristic>();
}