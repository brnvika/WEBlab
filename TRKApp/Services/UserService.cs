using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    public async Task CreateOrUpdateUserValidateAndThrowAsync(
        DataContext dataContext,
        User user,
        CancellationToken cancellationToken = default)
    {
        // Проверка на дубликат email
        if (await dataContext.Users.AnyAsync(u => u.UserId != user.UserId && u.Email == user.Email, cancellationToken))
        {
            throw new InvalidOperationException("Пользователь с таким email уже существует");
        }
        
        // Проверка на дубликат телефона
        if (await dataContext.Users.AnyAsync(u => u.UserId != user.UserId && u.NumberPhone == user.NumberPhone, cancellationToken))
        {
            throw new InvalidOperationException("Пользователь с таким номером телефона уже существует");
        }
    }
}