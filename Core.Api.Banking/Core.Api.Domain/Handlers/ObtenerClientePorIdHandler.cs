using Core.Api.Domain.Commands;
using Core.Api.Domain.Exceptions;
using Core.Api.Domain.Models;
using Core.Api.Domain.Queries;
using Core.Api.Domain.Repositories;
using FluentValidation;
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
        private readonly IValidator<ObtenerClientePorIdQuery> _validator;
        private readonly ILogger<ObtenerClientePorIdHandler> _logger;

        public ObtenerClientePorIdHandler(
            IClienteRepository clienteRepository, 
            IPersonaRepository personaRepository,
            IValidator<ObtenerClientePorIdQuery> validator,
            ILogger<ObtenerClientePorIdHandler> logger)
        {
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
            _validator = validator;
            _logger = logger;

        }
        public async Task<Cliente> Handle(ObtenerClientePorIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Obteniendo datos del cliente con id: {request.Id}");
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors);
            }

            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente is null)
            {
                throw new ClienteNotFoundException(request.Id);
            }

            cliente.Persona = await _personaRepository.GetByIdAsync(cliente.PersonaId);

            _logger.LogInformation($"Se encontraron datos del cliente con id: {request.Id}");
            return cliente;
        }
    }
}
