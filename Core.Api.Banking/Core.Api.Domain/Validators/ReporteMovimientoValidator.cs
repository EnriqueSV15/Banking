using Core.Api.Domain.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Validators
{
    public class ReporteMovimientoValidator : AbstractValidator<ReporteMovimientoQuery>
    {
        public ReporteMovimientoValidator()
        {
            RuleFor(query => query.FechaIni)
                .LessThanOrEqualTo(r => r.FechaFin)
                .WithMessage("La fecha de inicio no puede ser mayor a la fecha final");

            RuleFor(query => query.ClienteId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("El cliente no es válido");
        }
    }
}
