using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid ReviewId { get; set; }

    [Required, ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required, MaxLength(100)]
    public string StoreName { get; set; } = string.Empty;

    [Required, Range(1, 5)]
    public int StoreWork { get; set; }

    [Required, Range(1, 5)]
    public int ConsultantWork { get; set; }

    [Required, Range(1, 5)]
    public int CashierWork { get; set; }

    [Required, Range(1, 5)]
    public int QualityGoods { get; set; }

    [Required]
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    // Навигационное свойство для связи с User
    public User User { get; set; } = null!;
}