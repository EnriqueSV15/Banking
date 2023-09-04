using Core.Api.Domain.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Validators
{
    public class ObtenerClientePorIdValidator : AbstractValidator<ObtenerClientePorIdQuery>
    {
        public ObtenerClientePorIdValidator()
        {
            RuleFor(query => query.Id)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("El código del cliente no es válido");
        }
    }
}
