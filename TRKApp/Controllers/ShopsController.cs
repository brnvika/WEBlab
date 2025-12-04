using MediatR;
using Microsoft.AspNetCore.Mvc;
using TRKApp.Commands;

namespace TRKApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShopsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{shopName}")]
    public async Task<IActionResult> GetShop(string shopName)
    {
        var query = new GetShopQuery { ShopName = shopName };
        var shop = await _mediator.Send(query);

        if (shop == null)
        {
            return NotFound(new { error = "Магазин не найден" });
        }

        return Ok(shop);
    }
}
