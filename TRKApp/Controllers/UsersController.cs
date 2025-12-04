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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = await _mediator.Send(new CreateUserCommand { User = dto });
            return Ok(new { userId, message = "Регистрация успешна!" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка регистрации: " + ex.Message });
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

            Response.Cookies.Append("userId", result.UserId.ToString(), new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
            Response.Cookies.Append("userName", result.Name ?? "", new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
            Response.Cookies.Append("userSurname", result.Surname ?? "", new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
            Response.Cookies.Append("userEmail", result.Email ?? "", new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
            
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
