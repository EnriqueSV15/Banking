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
    public class ActualizarCuentaHandler : IRequestHandler<ActualizarCuentaCommand, bool>
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IValidator<ActualizarCuentaCommand> _validator;
        private readonly ILogger<ActualizarCuentaHandler> _logger;

        public ActualizarCuentaHandler(
            ICuentaRepository cuentaRepository,
            IValidator<ActualizarCuentaCommand> validator,
            ILogger<ActualizarCuentaHandler> logger)
        {
            _cuentaRepository = cuentaRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<bool> Handle(ActualizarCuentaCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Actualizando información de la cuenta {request.Numero}");
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors);
            }

            var cuenta = await _cuentaRepository.GetByNumeroAsync(request.Numero);
            if (cuenta is null)
            {
                throw new CuentaNotFoundException(request.Numero);
            }

            cuenta.Estado = request.Estado;

            var cuentaActualiza = await _cuentaRepository.UpdateAsync(cuenta);
            if (cuentaActualiza <= 0)
            {
                throw new BankingException("ACTUALIZACION_ERROR", new Exception("La persona no ha sido actualizada"));
            }

            _logger.LogInformation($"Se actualizó correctamente la cuenta {request.Numero}");
            return true;
        }
    }
}
