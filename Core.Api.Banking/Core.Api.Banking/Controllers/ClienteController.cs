using Core.Api.Domain.Commands;
using Core.Api.Domain.DTOs;
using Core.Api.Domain.Handlers;
using Core.Api.Domain.Models;
using Core.Api.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> ObtenerClientePorId(int id)
        {
            var result = await _mediator.Send(new ObtenerClientePorIdQuery { Id = id });

            return Ok(result.ObtenerDTO());
        }

        [HttpPost]
        public async Task<ActionResult> CrearCliente(CrearClienteCommand request)
        {
            var result = await _mediator.Send(request);

            if(result is not null)
            {
                return Ok(new { Message = $"Cliente creado con el id:{result.ClienteId}" });
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCliente(int id)
        {
            var result = await _mediator.Send(new EliminarClienteCommand { Id = id });

            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCliente([FromRoute] int id, [FromBody] ActualizarClienteCommand request)
        {
            request.Id = id;

            var result = await _mediator.Send(request);

            if (result)
            {
                return Ok(new { Message = $"Cliente actualizado con el id:{id}" });
            }

            return BadRequest();
        }
    }
}
