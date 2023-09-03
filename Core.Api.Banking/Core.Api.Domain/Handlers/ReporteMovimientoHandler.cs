using Core.Api.Domain.Commands;
using Core.Api.Domain.DTOs;
using Core.Api.Domain.Enums;
using Core.Api.Domain.Exceptions;
using Core.Api.Domain.Models;
using Core.Api.Domain.Queries;
using Core.Api.Domain.Repositories;
using Core.Api.Domain.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Handlers
{
    public class ReporteMovimientoHandler : IRequestHandler<ReporteMovimientoQuery, List<ReporteMovimientoDTO>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IValidator<ReporteMovimientoQuery> _validator;
        private readonly ILogger<ReporteMovimientoHandler> _logger;

        public ReporteMovimientoHandler(
            IClienteRepository clienteRepository, 
            ICuentaRepository cuentaRepository, 
            IMovimientoRepository movimientoRepository,
            IValidator<ReporteMovimientoQuery> validator,
            ILogger<ReporteMovimientoHandler> logger)
        {
            _clienteRepository = clienteRepository;
            _cuentaRepository = cuentaRepository;
            _movimientoRepository = movimientoRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<List<ReporteMovimientoDTO>> Handle(ReporteMovimientoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Generando reporte de movimientos");

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors);
            }

            var listResult = await _movimientoRepository.ObtenerMovimientosPorRangoFechasAsync(request.FechaIni, request.FechaFin, request.ClienteId);

            var result = listResult.Select(e =>
            {
                return new ReporteMovimientoDTO
                {
                    Fecha = e.Fecha.Date.ToString("dd/MM/yyyy"),
                    Cliente = e.Cuenta.Cliente.Persona.Nombre,
                    Numero = e.Cuenta.Numero,
                    Tipo = Enum.GetName(e.Cuenta.Tipo) ?? "",
                    SaldoInicial = e.Cuenta.SaldoInicial,
                    Estado = e.Cuenta.Estado.ToString(),
                    Movimiento = e.Valor,
                    SaldoDisponible = e.Saldo
                };
            });

            _logger.LogInformation($"Se encontraron {result.Count()} movimientos");
            return result.ToList();
        }
    }
}
