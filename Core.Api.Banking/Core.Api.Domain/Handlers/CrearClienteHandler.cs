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
    public class CrearClienteHandler : IRequestHandler<CrearClienteCommand, Cliente>
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<CrearClienteCommand> _validator;
        private readonly ILogger<CrearClienteHandler> _logger;

        public CrearClienteHandler(
            IPersonaRepository personaRepository, 
            IClienteRepository clienteRepository,
            IValidator<CrearClienteCommand> validator,
            ILogger<CrearClienteHandler> logger)
        {
            _personaRepository = personaRepository;
            _clienteRepository = clienteRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Cliente> Handle(CrearClienteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creando cliente");
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors);
            }

            var nuevaPersona = new Persona
            {
                Nombre = request.Nombre,
                Genero = request.Genero,
                Edad = request.Edad,
                Identificacion = request.Identificacion,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
            };
            var persona = await _personaRepository.AddAsync(nuevaPersona);

            var nuevoCliente = new Cliente
            {
                Contrasena = request.Contrasena,
                Estado = true,
                PersonaId = persona.PersonaId,
            };

            var result = await _clienteRepository.AddAsync(nuevoCliente);

            _logger.LogInformation($"Se creó correctamente el cliente {nuevaPersona.Nombre}");
            return result;
        }
    }
}
