using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TRKApp.Services;

namespace TRKApp.Models.Commands;

/// <summary>
/// Обновление данных пользователя
/// </summary>
public sealed class UpdateUserCommand : IRequest<bool>
{
    /// <summary>
    /// ID пользователя для обновления
    /// </summary>
    [FromRoute]
    [Required]
    public Guid UserId { get; init; }

    /// <summary>
    /// Обновленные данные пользователя
    /// </summary>
    [FromBody]
    [Required]
    public UpdateUserDto User { get; init; }
}

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly DataContext _dataContext;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserCommandHandler(DataContext dataContext, IPasswordHasher passwordHasher)
    {
        _dataContext = dataContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataContext.Users
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null)
        {
            return false;
        }

        // Обновление полей
        user.Surname = request.User.Surname;
        user.Name = request.User.Name;
        user.NumberPhone = request.User.NumberPhone;
        user.Email = request.User.Email;
        user.BirthDate = request.User.BirthDate;
        user.Gender = request.User.Gender;
        user.Subscribe = request.User.Subscribe;

        // Обновление пароля, если указан новый
        if (!string.IsNullOrEmpty(request.User.Password))
        {
            user.PasswordHash = _passwordHasher.HashPassword(request.User.Password);
        }

        await _dataContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
