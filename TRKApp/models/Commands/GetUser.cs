using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TRKApp.Models.Commands;

/// <summary>
/// Запрос данных пользователя
/// </summary>
public sealed class GetUserQuery : IRequest<UserProfileDto?>
{
    public Guid UserId { get; init; }
}

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserProfileDto?>
{
    private readonly DataContext _dataContext;

    public GetUserQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<UserProfileDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dataContext.Users
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null)
        {
            return null;
        }

        return new UserProfileDto
        {
            UserId = user.UserId,
            Surname = user.Surname,
            Name = user.Name,
            NumberPhone = user.NumberPhone,
            Email = user.Email,
            BirthDate = user.BirthDate,
            Gender = user.Gender,
            Subscribe = user.Subscribe
        };
    }
}

/// <summary>
/// DTO профиля пользователя
/// </summary>
public class UserProfileDto
{
    public Guid UserId { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NumberPhone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public bool Subscribe { get; set; }
}
