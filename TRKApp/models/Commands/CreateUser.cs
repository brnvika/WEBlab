using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public CreateUserDto User { get; init; }
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
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Surname = request.User.Surname,
            Name = request.User.Name,
            NumberPhone = request.User.NumberPhone,
            Email = request.User.Email,
            PasswordHash = _passwordHasher.HashPassword(request.User.Password),
            BirthDate = request.User.BirthDate,
            Gender = request.User.Gender,
            Subscribe = request.User.Subscribe
        };

        await _dataContext.Users.AddAsync(user, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return user.UserId;
    }
}
