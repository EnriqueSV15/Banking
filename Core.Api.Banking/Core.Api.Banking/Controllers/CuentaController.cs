using Core.Api.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CuentaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CrearCuenta(CrearCuentaCommand request)
        {
            var result = await _mediator.Send(request);

            if (result is not null)
            {
                return Ok(new { Message = $"La Cuenta {result.Numero} ha sido creada con el id:{result.CuentaId}" });
            }

            return BadRequest();
        }

        [HttpPut("{numero}")]
        public async Task<ActionResult> ActualizarCuenta([FromRoute] string numero, [FromBody] ActualizarCuentaCommand request)
        {
            request.Numero = numero;

            var result = await _mediator.Send(request);

            if (result)
            {
                return Ok(new { Message = $"Cuenta actualizada con el número:{numero}" });
            }

            return BadRequest();
        }
    }
}
