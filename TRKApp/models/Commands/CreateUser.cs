using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TRKApp.Services;

namespace TRKApp.Models.Commands;

/// <summary>
/// Создание пользователя при регистрации
/// </summary>
public sealed class CreateUserCommand : IRequest<Guid>
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    [FromBody]
    [Required]
    public required CreateUserDto User { get; init; }
}

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly DataContext _dataContext;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(DataContext dataContext, IPasswordHasher passwordHasher)
    {
        _dataContext = dataContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Проверка уникальности email
        var emailExists = await _dataContext.Users
            .AnyAsync(u => u.Email == request.User.Email, cancellationToken);
        if (emailExists)
        {
            throw new InvalidOperationException("Пользователь с таким email уже зарегистрирован");
        }

        // Проверка уникальности номера телефона
        var phoneExists = await _dataContext.Users
            .AnyAsync(u => u.NumberPhone == request.User.NumberPhone, cancellationToken);
        if (phoneExists)
        {
            throw new InvalidOperationException("Пользователь с таким номером телефона уже зарегистрирован");
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Surname = request.User.Surname,
            Name = request.User.Name,
            NumberPhone = request.User.NumberPhone,
            Email = request.User.Email,
            PasswordHash = _passwordHasher.HashPassword(request.User.Password!),
            BirthDate = request.User.BirthDate.Kind == DateTimeKind.Unspecified 
                ? DateTime.SpecifyKind(request.User.BirthDate, DateTimeKind.Utc)
                : request.User.BirthDate.ToUniversalTime(),
            Gender = request.User.Gender,
            Subscribe = request.User.Subscribe
        };

        await _dataContext.Users.AddAsync(user, cancellationToken);
        
        try
        {
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            // Обработка ошибок базы данных
            if (ex.InnerException?.Message.Contains("duplicate key") == true)
            {
                throw new InvalidOperationException("Пользователь с такими данными уже существует");
            }
            throw new InvalidOperationException("Ошибка при сохранении данных. Попробуйте еще раз.");
        }

        return user.UserId;
    }
}
