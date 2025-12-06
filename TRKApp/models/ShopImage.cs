using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRKApp.Models
{
    public class ShopImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid ShopId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ImagePath { get; set; } = string.Empty;

        [ForeignKey(nameof(ShopId))]
        public Shop? Shop { get; set; }
    }
}
