using Core.Api.Domain.Commands;
using Core.Api.Domain.Enums;
using Core.Api.Domain.Exceptions;
using Core.Api.Domain.Models;
using Core.Api.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Handlers
{
    public class InsertarMovimientoHandler : IRequestHandler<InsertarMovimientoCommand, Movimiento>
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IValidator<InsertarMovimientoCommand> _validator;
        private readonly Settings _settings;
        private readonly ILogger<InsertarMovimientoHandler> _logger;

        public InsertarMovimientoHandler(
            ICuentaRepository cuentaRepository, 
            IMovimientoRepository movimientoRepository,
            IValidator<InsertarMovimientoCommand> validator,
            IOptions<Settings> options,
            ILogger<InsertarMovimientoHandler> logger)
        {
            _cuentaRepository = cuentaRepository;
            _movimientoRepository = movimientoRepository;
            _validator = validator;
            _settings = options.Value;
            _logger = logger;
        }
        public async Task<Movimiento> Handle(InsertarMovimientoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Insertando movimiento de la cuenta: {request.Numero}");
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

            var listaMovimiento = await _movimientoRepository.ObtenerMovimientosPorCuentaAsync(cuenta.CuentaId, request.Fecha);

            var totalMovimiento = listaMovimiento.Sum(x => x.Valor);
            var totalDebito = listaMovimiento.Where(x => x.Tipo == TipoMovimiento.Debito).Sum(x => x.Valor);

            var saldoDisponible = cuenta.SaldoInicial + totalMovimiento;

            var tipoMovimiento = request.Valor < 0 ? TipoMovimiento.Debito : TipoMovimiento.Credito;

            //if ((saldoDisponible <= 0 && tipoMovimiento == TipoMovimiento.Debito) || (request.Valor < 0 && saldoDisponible < Math.Abs(request.Valor)))
            if(tipoMovimiento == TipoMovimiento.Debito && saldoDisponible < Math.Abs(request.Valor))
            {
                throw new SaldoNoDisponibleException(request.Numero);
            }

            if (tipoMovimiento == TipoMovimiento.Debito &&  Math.Abs(totalDebito + request.Valor) > _settings.LimiteDiario)
            {
                throw new LimiteRetiroException();
            }

            var nuevoMovimiento = new Movimiento
            {
                Fecha = request.Fecha,
                Tipo = tipoMovimiento,
                Valor = request.Valor,
                Saldo = saldoDisponible + request.Valor,
                CuentaId = cuenta.CuentaId,
            };

            var result = await _movimientoRepository.AddAsync(nuevoMovimiento);

            _logger.LogInformation($"Se insertó un movimiento con el monto de {nuevoMovimiento.Valor} en la cuenta: {request.Numero}");
            return result;
        }
    }
}
