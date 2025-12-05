using Microsoft.AspNetCore.Mvc;
using MediatR;
using TRKApp.Models.Commands;

namespace TRKApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
    {
        Console.WriteLine($"Получен запрос регистрации для email: {dto?.Email}");
        
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState невалиден:");
            foreach (var error in ModelState)
            {
                Console.WriteLine($"  {error.Key}: {string.Join(", ", error.Value?.Errors.Select(e => e.ErrorMessage) ?? Array.Empty<string>())}");
            }
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => 
                    {
                        // Переводим стандартные сообщения на русский
                        if (e.ErrorMessage.Contains("required")) return "Обязательное поле";
                        if (e.ErrorMessage.Contains("invalid")) return "Неверный формат";
                        return e.ErrorMessage;
                    }).ToArray() ?? Array.Empty<string>()
                );
            return BadRequest(new { error = "Проверьте правильность заполнения полей", errors });
        }

        try
        {
            var userId = await _mediator.Send(new CreateUserCommand { User = dto });
            return Ok(new { userId, message = "Регистрация успешна!" });
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"InvalidOperationException: {ex.Message}");
            // Проверяем тип ошибки для более точного сообщения
            if (ex.Message.Contains("email") || ex.Message.Contains("Email"))
            {
                return Conflict(new { error = "Пользователь с таким email уже зарегистрирован" });
            }
            else if (ex.Message.Contains("phone") || ex.Message.Contains("Phone"))
            {
                return Conflict(new { error = "Пользователь с таким номером телефона уже зарегистрирован" });
            }
            else
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            // Логируем ошибку для отладки
            Console.WriteLine($"Registration error: {ex.Message}");
            return StatusCode(500, new { error = "Произошла ошибка при регистрации. Попробуйте позже." });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _mediator.Send(new LoginUserCommand { User = dto });

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            };
            
            Response.Cookies.Append("userId", result.UserId.ToString(), cookieOptions);
            Response.Cookies.Append("userName", result.Name ?? "", cookieOptions);
            Response.Cookies.Append("userSurname", result.Surname ?? "", cookieOptions);
            Response.Cookies.Append("userEmail", result.Email ?? "", cookieOptions);
            
            return Ok(new 
            { 
                userId = result.UserId,
                name = result.Name,
                surname = result.Surname,
                email = result.Email,
                message = "Вход выполнен успешно!" 
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка авторизации: " + ex.Message });
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("userId");
        Response.Cookies.Delete("userName");
        Response.Cookies.Delete("userSurname");
        Response.Cookies.Delete("userEmail");
        
        return Ok(new { message = "Выход выполнен успешно" });
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userIdCookie = Request.Cookies["userId"];
        if (string.IsNullOrEmpty(userIdCookie) || !Guid.TryParse(userIdCookie, out var userId))
        {
            return Unauthorized(new { error = "Пользователь не авторизован" });
        }

        var user = await _mediator.Send(new GetUserQuery { UserId = userId });
        if (user == null)
        {
            return NotFound(new { error = "Пользователь не найден" });
        }

        return Ok(user);
    }
}
