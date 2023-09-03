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
    public class ActualizarClienteHandler : IRequestHandler<ActualizarClienteCommand, bool>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<ActualizarClienteCommand> _validator;
        private readonly ILogger<ActualizarClienteHandler> _logger;

        public ActualizarClienteHandler(
            IPersonaRepository personaRepository, 
            IClienteRepository clienteRepository,
            IValidator<ActualizarClienteCommand> validator,
            ILogger<ActualizarClienteHandler> logger)
        {
            _personaRepository = personaRepository;
            _clienteRepository = clienteRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<bool> Handle(ActualizarClienteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Actualizando cliente");
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

            var persona = await _personaRepository.GetByIdAsync(cliente.PersonaId);
            if (persona is null)
            {
                throw new PersonaNotFoundException(request.Id);
            }

            persona.Nombre = request.Nombre;
            persona.Genero = request.Genero;
            persona.Edad = request.Edad;
            persona.Identificacion = request.Identificacion;
            persona.Direccion = request.Direccion;
            persona.Telefono = request.Telefono;

            cliente.Estado = request.Estado;

            var clienteActualiza = await _clienteRepository.UpdateAsync(cliente);
            if (clienteActualiza <= 0)
            {
                throw new BankingException("ACTUALIZACION_ERROR", new Exception($"El cliente con el id: {request.Id} no ha sido actualizado"));
            }

            var personaActualiza = await _personaRepository.UpdateAsync(persona);
            if (personaActualiza <= 0)
            {
                throw new BankingException("ACTUALIZACION_ERROR", new Exception("La persona no ha sido actualizada"));
            }

            _logger.LogInformation($"Se actualizó el cliente con id: {request.Id}");
            return true;
        }
    }
}
