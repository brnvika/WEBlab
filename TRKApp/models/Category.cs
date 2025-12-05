using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public Guid CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string CategoryName { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    // Навигационное свойство
    public ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
