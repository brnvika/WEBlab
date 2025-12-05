using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Question { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}