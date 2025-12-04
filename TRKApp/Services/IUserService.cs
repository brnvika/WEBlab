public interface IUserService
{
    /// <summary>
    /// Проверка модели пользователя на возможность создания или редактирования
    /// </summary>
    /// <param name="dataContext">Контекст базы данных</param>
    /// <param name="user">Пользователь</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task CreateOrUpdateUserValidateAndThrowAsync(
        DataContext dataContext,
        User user,
        CancellationToken cancellationToken = default);
}