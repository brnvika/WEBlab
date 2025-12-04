using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TRKApp.Services;

namespace TRKApp.Models.Commands;

/// <summary>
/// Авторизация пользователя
/// </summary>
public sealed class LoginUserCommand : IRequest<LoginResult>
{
    /// <summary>
    /// Данные для входа
    /// </summary>
    [FromBody]
    [Required]
    public required LoginUserDto User { get; init; }
}

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResult>
{
    private readonly DataContext _dataContext;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserCommandHandler(DataContext dataContext, IPasswordHasher passwordHasher)
    {
        _dataContext = dataContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var emailOrPhone = request.User.EmailOrPhone!;
        var user = await _dataContext.Users
            .FirstOrDefaultAsync(u => u.Email == emailOrPhone || u.NumberPhone == emailOrPhone, cancellationToken);

        if (user == null)
        {
            throw new InvalidOperationException("Неверный email/телефон");
        }

        if (!_passwordHasher.VerifyPassword(user.PasswordHash, request.User.Password!))
        {
            throw new InvalidOperationException("Неверный пароль");
        }

        return new LoginResult
        {
            UserId = user.UserId,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email
        };
    }
}

/// <summary>
/// Результат авторизации
/// </summary>
public class LoginResult
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
}
