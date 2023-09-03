using Core.Api.Domain.Commands;
using Core.Api.Domain.Exceptions;
using Core.Api.Domain.Models;
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
    public class CrearCuentaHandler : IRequestHandler<CrearCuentaCommand, Cuenta>
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<CrearCuentaCommand> _validator;
        private readonly ILogger<CrearCuentaHandler> _logger;

        public CrearCuentaHandler(
            ICuentaRepository cuentaRepository, 
            IClienteRepository clienteRepository,
            IValidator<CrearCuentaCommand> validator,
            ILogger<CrearCuentaHandler> logger)
        {
            _cuentaRepository = cuentaRepository;
            _clienteRepository = clienteRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Cuenta> Handle(CrearCuentaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creando la cuenta {request.Numero} para el cliente con id: {request.ClienteId}");
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors);
            }

            var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
            if (cliente is null)
            {
                throw new ClienteNotFoundException(request.ClienteId);
            }

            var nuevaCuenta = new Cuenta
            {
                Numero = request.Numero,
                Tipo = request.Tipo,
                SaldoInicial = request.Saldo,
                Estado = true,
                ClienteId = request.ClienteId,
            };

            var result = await _cuentaRepository.AddAsync(nuevaCuenta);

            _logger.LogInformation($"Se registró correctamente la cuenta {request.Numero} para el cliente con id: {request.ClienteId}");
            return result;
        }
    }
}
