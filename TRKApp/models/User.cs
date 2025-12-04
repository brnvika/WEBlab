using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid UserId { get; set; }
    
    [Required, MaxLength(ModelConstants.UserSurnameMaxLength)]
    public string Surname { get; set; } = string.Empty;
    
    [Required, MaxLength(ModelConstants.UserNameMaxLength)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string NumberPhone { get; set; } = string.Empty;
    
    [Required, MaxLength(ModelConstants.UserEmailMaxLength)]
    public string Email { get; set; } = string.Empty;
    
    [Required, Length(ModelConstants.UserPasswordHashMinLength, ModelConstants.UserPasswordHashMaxLength)]
    public string PasswordHash { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public string? Gender { get; set; } 
    
    public bool Subscribe { get; set; }
}