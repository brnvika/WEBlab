using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TRKApp.Models.Commands;

/// <summary>
/// Удаление пользователя
/// </summary>
public sealed class DeleteUserCommand : IRequest<bool>
{
    /// <summary>
    /// ID пользователя для удаления
    /// </summary>
    [FromRoute]
    [Required]
    public Guid UserId { get; init; }
}

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly DataContext _dataContext;

    public DeleteUserCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dataContext.Users
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null)
        {
            return false;
        }

        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
