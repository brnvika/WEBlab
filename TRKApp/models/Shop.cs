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

    // Навигационное свойство для характеристик
    public ICollection<ShopCharacteristic> Characteristics { get; set; } = new List<ShopCharacteristic>();
}