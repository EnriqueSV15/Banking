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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Handlers
{
    public class EliminarClienteHandler : IRequestHandler<EliminarClienteCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<EliminarClienteCommand> _validator;
        private readonly ILogger<EliminarClienteHandler> _logger;

        public EliminarClienteHandler(
            IPersonaRepository personaRepository, 
            IClienteRepository clienteRepository,
            IValidator<EliminarClienteCommand> validator,
            ILogger<EliminarClienteHandler> logger)
        {
            _personaRepository = personaRepository;
            _clienteRepository = clienteRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<bool> Handle(EliminarClienteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Eliminando el cliente con id: {request.Id}");
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors);
            }

            var cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente is null)
            {
                _logger.LogInformation($"No se encontró el cliente con id: {request.Id}");
                throw new ClienteNotFoundException(request.Id);
            }

            var deleteClienteResult = await _clienteRepository.DeleteAsync(cliente.ClienteId);

            if(deleteClienteResult <= 0)
            {
                throw new ClienteNotFoundException(request.Id);
            }

            var deletePersonaResult = await _personaRepository.DeleteAsync(cliente.PersonaId);
            if(deletePersonaResult <= 0)
            {
                throw new PersonaNotFoundException(cliente.PersonaId);
            }

            _logger.LogInformation($"Se eliminó correctamente el cliente con id: {request.Id}");
            return true;
        }
    }
}
