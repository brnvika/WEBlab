using Microsoft.AspNetCore.Mvc;
using MediatR;
using TRKApp.Models.Commands;

namespace TRKApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создание отзыва (только для авторизованных пользователей)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
    {
        // Проверка авторизации через cookie
        var userIdCookie = Request.Cookies["userId"];
        if (string.IsNullOrEmpty(userIdCookie) || !Guid.TryParse(userIdCookie, out var userId))
        {
            return Unauthorized(new { error = "Необходима авторизация для оставления отзыва" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var reviewId = await _mediator.Send(new CreateReviewCommand 
            { 
                UserId = userId,
                Review = dto 
            });
            
            return Ok(new { reviewId, message = "Отзыв успешно добавлен!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка создания отзыва: " + ex.Message });
        }
    }

    /// <summary>
    /// Получение отзывов по магазину
    /// </summary>
    [HttpGet("{storeName}")]
    public async Task<IActionResult> GetReviewsByStore(string storeName)
    {
        try
        {
            var reviews = await _mediator.Send(new GetReviewsByStoreQuery { StoreName = storeName });
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка получения отзывов: " + ex.Message });
        }
    }
}
