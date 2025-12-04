using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ShopCharacteristic
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid CharacteristicId { get; set; }

    [Required, ForeignKey("Shop")]
    public Guid ShopId { get; set; }

    [Required, MaxLength(100)]
    public string Parameter { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    // Навигационное свойство
    public Shop Shop { get; set; } = null!;
}
