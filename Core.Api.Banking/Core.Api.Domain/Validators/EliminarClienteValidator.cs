using Core.Api.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Validators
{
    public class EliminarClienteValidator : AbstractValidator<EliminarClienteCommand>
    {
        public EliminarClienteValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("El código del cliente no es válido");
        }
    }
}
