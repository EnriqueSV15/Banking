using Core.Api.Domain.Commands;
using Core.Api.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Validators
{
    public class ActualizarCuentaValidator : AbstractValidator<ActualizarCuentaCommand>
    {
        public ActualizarCuentaValidator()
        {
            RuleFor(command => command.Numero)
                .NotEmpty()
                .NotNull()
                .WithMessage("El número no es válido");

            RuleFor(command => command.Estado)
                .NotEmpty()
                .NotNull()
                .WithMessage("El estado no es válido");
        }
    }
}
