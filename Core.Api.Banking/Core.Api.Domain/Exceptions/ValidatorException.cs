using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Exceptions
{
    public class ValidatorException : BankingException
    {
        public ValidatorException(List<ValidationFailure> errors) : base("ERROR_DE_VALIDACION", new Exception(string.Join("\n ", errors.Select(x => x.ErrorMessage))))
        {
        }
    }
}
