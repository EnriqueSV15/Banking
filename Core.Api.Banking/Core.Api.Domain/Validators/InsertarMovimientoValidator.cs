using Core.Api.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Validators
{
    public class InsertarMovimientoValidator : AbstractValidator<InsertarMovimientoCommand>
    {
        public InsertarMovimientoValidator()
        {
            RuleFor(command => command.Fecha)
                .NotEmpty()
                .NotNull()
                .WithMessage("El fecha no es válida");

            RuleFor(command => command.Valor)
                .NotEmpty()
                .NotNull()
                .NotEqual(0)
                .WithMessage("El valor no es válido");

            RuleFor(command => command.Numero)
                .NotEmpty()
                .NotNull()
                .WithMessage("El número de cuenta no es válido");
        }
    }
}
