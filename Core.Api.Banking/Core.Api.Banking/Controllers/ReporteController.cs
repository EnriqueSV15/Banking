using Core.Api.Domain.DTOs;
using Core.Api.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReporteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ReporteMovimientoDTO>> ReporteMovimientos([FromQuery] DateTime fechaIni, [FromQuery] DateTime fechaFin, [FromQuery] int clienteId)
        {
            var result = await _mediator.Send(new ReporteMovimientoQuery { 
                FechaIni = fechaIni,
                FechaFin = fechaFin,
                ClienteId = clienteId
            });

            return Ok(result);
        }
    }
}
