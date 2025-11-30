using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands;

/// <summary>
/// DTO модель для создания пользователя
/// </summary>
public sealed class CreateUserDto
{
    /// <summary>
    /// Фамилия
    /// </summary>
    [Required]
    public string Surname { get; set; } = string.Empty;

    /// <summary>
    /// Имя
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required]
    [Phone]
    public string NumberPhone { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public string Gender { get; set; } = string.Empty;

    /// <summary>
    /// Подписка на рассылку
    /// </summary>
    public bool Subscribe { get; set; }
}
