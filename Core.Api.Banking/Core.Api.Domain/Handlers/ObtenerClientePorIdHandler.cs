using Core.Api.Domain.Models;
using Core.Api.Domain.Queries;
using Core.Api.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Handlers
{
    public class ObtenerClientePorIdHandler : IRequestHandler<ObtenerClientePorIdQuery, Cliente>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly ILogger<ObtenerClientePorIdHandler> _logger;

        public ObtenerClientePorIdHandler(
            IClienteRepository clienteRepository, 
            IPersonaRepository personaRepository,
            ILogger<ObtenerClientePorIdHandler> logger)
        {
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
            _logger = logger;

        }
        public async Task<Cliente> Handle(ObtenerClientePorIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Obteniendo datos del cliente con id: {request.Id}");

            var cliente = await _clienteRepository.GetByIdAsync(request.Id);

            cliente.Persona = await _personaRepository.GetByIdAsync(cliente.PersonaId);

            _logger.LogInformation($"Se encontraron datos del cliente con id: {request.Id}");
            return cliente;
        }
    }
}
