using Core.Api.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Validators
{
    public class CrearClienteValidator : AbstractValidator<CrearClienteCommand>
    {
        public CrearClienteValidator()
        {
            RuleFor(command => command.Nombre)
                .NotEmpty()
                .NotNull()
                .WithMessage("El nombre no es válido");

            RuleFor(command => command.Genero)
                .NotEmpty()
                .NotNull()
                .Length(1)
                .Must(genero => genero == "M" || genero == "F")
                .WithMessage("El género no es válido");

            RuleFor(command => command.Edad)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(18)
                .WithMessage("El edad no es válida");

            RuleFor(command => command.Identificacion)
                .NotEmpty()
                .NotNull()
                .Length(8)
                .WithMessage("La identificación no es válida");

            RuleFor(command => command.Direccion)
                .NotEmpty()
                .NotNull()
                .WithMessage("El dirección no es válido");

            RuleFor(command => command.Telefono)
                .NotEmpty()
                .NotNull()
                .Length(9)
                .WithMessage("El teléfono no es válido");

            RuleFor(command => command.Contrasena)
                .NotEmpty()
                .NotNull()
                .WithMessage("La contraseña no es válido");
        }
    }
}
