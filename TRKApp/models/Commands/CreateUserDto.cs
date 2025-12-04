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
    [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
    public string? Surname { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    [Required(ErrorMessage = "Имя обязательно для заполнения")]
    public string? Name { get; init; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required(ErrorMessage = "Номер телефона обязателен для заполнения")]
    [Phone]
    public string? NumberPhone { get; init; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    [Required(ErrorMessage = "Электронная почта обязательна для заполнения")]
    [EmailAddress]
    public string? Email { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [MinLength(8, ErrorMessage = "Пароль должен содержать не менее 8 символов")]
    [MaxLength(128, ErrorMessage = "Пароль должен содержать не более 128 символов")]
    [RegularExpression(@"^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/?])(?!.*\s)[a-zA-Zа-яА-Я\d!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/?]{8,128}$", 
        ErrorMessage = "Пароль должен содержать: заглавную и строчную буквы (латиница/кириллица), цифру, спецсимвол, без пробелов")]
    public string? Password { get; init; }

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    [Required(ErrorMessage = "Подтверждение пароля обязательно для заполнения")]
    public string? ConfirmPassword { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; init; }

    /// <summary>
    /// Пол
    /// </summary>
    public string? Gender { get; init; }

    /// <summary>
    /// Подписка на рассылку
    /// </summary>
    public bool Subscribe { get; init; }
}
