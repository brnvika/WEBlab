using Microsoft.AspNetCore.Mvc;
using MediatR;
using TRKApp.Models.Commands;

namespace TRKApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение корзины пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userIdCookie = Request.Cookies["userId"];
        if (string.IsNullOrEmpty(userIdCookie) || !Guid.TryParse(userIdCookie, out var userId))
        {
            return Unauthorized(new { error = "Необходима авторизация для просмотра корзины" });
        }

        try
        {
            var cart = await _mediator.Send(new GetCartQuery { UserId = userId });
            
            if (cart == null)
            {
                return Ok(new { items = new List<object>(), totalPrice = 0 });
            }
            
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка получения корзины: " + ex.Message });
        }
    }

    /// <summary>
    /// Добавление товара в корзину
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
    {
        var userIdCookie = Request.Cookies["userId"];
        if (string.IsNullOrEmpty(userIdCookie) || !Guid.TryParse(userIdCookie, out var userId))
        {
            return Unauthorized(new { error = "Необходима авторизация для добавления в корзину" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var cartId = await _mediator.Send(new AddToCartCommand 
            { 
                UserId = userId,
                Item = dto
            });
            
            return Ok(new { cartId, message = "Площадь добавлена в корзину!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка добавления в корзину: " + ex.Message });
        }
    }

    /// <summary>
    /// Удаление товара из корзины
    /// </summary>
    [HttpDelete("{itemId}")]
    public async Task<IActionResult> RemoveFromCart(Guid itemId)
    {
        var userIdCookie = Request.Cookies["userId"];
        if (string.IsNullOrEmpty(userIdCookie) || !Guid.TryParse(userIdCookie, out var userId))
        {
            return Unauthorized(new { error = "Необходима авторизация" });
        }

        try
        {
            var success = await _mediator.Send(new RemoveFromCartCommand 
            { 
                UserId = userId,
                ItemId = itemId
            });
            
            if (!success)
            {
                return NotFound(new { error = "Элемент не найден в корзине" });
            }
            
            return Ok(new { message = "Элемент удалён из корзины" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка удаления из корзины: " + ex.Message });
        }
    }
}
