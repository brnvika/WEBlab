using MediatR;
using Microsoft.AspNetCore.Mvc;
using TRKApp.Models.Commands;

namespace TRKApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    
                    return BadRequest(new { message = string.Join("; ", errors) });
                }

                var command = new CreateContact
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Question = dto.Question
                };

                var result = await _mediator.Send(command);

                return Ok(new { 
                    message = "Ваше сообщение успешно отправлено! Мы свяжемся с вами в ближайшее время.",
                    contactId = result.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при отправке сообщения. Попробуйте позже." });
            }
        }
    }
}