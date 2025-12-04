using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands;

/// <summary>
/// DTO модель для обновления пользователя
/// </summary>
public sealed class UpdateUserDto
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
    /// Пароль (опционально, только если нужно изменить)
    /// </summary>
    [MinLength(8, ErrorMessage = "Пароль должен содержать не менее 8 символов")]
    [MaxLength(128, ErrorMessage = "Пароль должен содержать не более 128 символов")]
    [RegularExpression(@"^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/?])(?!.*\s)[a-zA-Zа-яА-Я\d!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/?]{8,128}$", 
        ErrorMessage = "Пароль должен содержать: заглавную и строчную буквы (латиница/кириллица), цифру, спецсимвол, без пробелов")]
    public string? Password { get; init; }

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
