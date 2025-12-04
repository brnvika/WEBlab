using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands;

/// <summary>
/// DTO модель для авторизации пользователя
/// </summary>
public sealed class LoginUserDto
{
    /// <summary>
    /// Email или номер телефона
    /// </summary>
    [Required(ErrorMessage = "Email или номер телефона обязателен для заполнения")]
    public string? EmailOrPhone { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    public string? Password { get; init; }
}
