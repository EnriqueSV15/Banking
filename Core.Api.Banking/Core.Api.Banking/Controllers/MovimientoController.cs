using Core.Api.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovimientoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> InsertarMovimiento(InsertarMovimientoCommand request)
        {
            var result = await _mediator.Send(request);

            if (result is not null)
            {
                return Ok(new { Message = $"El movimiento de la cuenta {request.Numero} ha sido creada" });
            }

            return BadRequest();
        }
    }
}
