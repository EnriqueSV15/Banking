using Core.Api.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Validators
{
    public class CrearCuentaValidator : AbstractValidator<CrearCuentaCommand>
    {
        public CrearCuentaValidator()
        {
            RuleFor(command => command.Numero)
                .NotEmpty()
                .NotNull()
                .WithMessage("El número de cuenta no es válido");

            RuleFor(command => command.Saldo)
                .NotEmpty()
                .NotNull()
                .WithMessage("El saldo no es válido");

            RuleFor(command => command.Estado)
                .NotEmpty()
                .NotNull()
                .WithMessage("El estado no es válido");

            RuleFor(command => command.ClienteId)
                .NotEmpty()
                .NotNull()
                .WithMessage("El cliente no es válido");
        }
    }
}
