using Microsoft.AspNetCore.Mvc;
using MediatR;
using TRKApp.Models.Commands;

namespace TRKApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalSpacesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RentalSpacesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение списка доступных торговых площадей
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetRentalSpaces([FromQuery] string? category, [FromQuery] bool? availableOnly)
    {
        try
        {
            var spaces = await _mediator.Send(new GetRentalSpacesQuery 
            { 
                Category = category,
                AvailableOnly = availableOnly ?? true
            });
            
            return Ok(spaces);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка получения списка площадей: " + ex.Message });
        }
    }

    /// <summary>
    /// Получение торговой площади по ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRentalSpaceById(Guid id)
    {
        try
        {
            var spaces = await _mediator.Send(new GetRentalSpacesQuery { AvailableOnly = false });
            var space = spaces.FirstOrDefault(s => s.Id == id);
            
            if (space == null)
            {
                return NotFound(new { error = "Торговая площадь не найдена" });
            }
            
            return Ok(space);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ошибка получения площади: " + ex.Message });
        }
    }
}
