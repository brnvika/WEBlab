using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid UserId { get; set; }
    
    [Required, MaxLength(ModelConstants.UserSurnameMaxLength)]
    public string? Surname { get; set; }
    
    [Required, MaxLength(ModelConstants.UserNameMaxLength)]
    public string? Name { get; set; }
    
    [Required]
    public string? NumberPhone { get; set; }
    
    [Required, MaxLength(ModelConstants.UserEmailMaxLength)]
    public string? Email { get; set; }
    
    [Required, Length(ModelConstants.UserPasswordHashMinLength, ModelConstants.UserPasswordHashMaxLength)]
    public string? PasswordHash { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string? Gender { get; set; } 
    
    public bool Subscribe { get; set; }
}