using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Exceptions
{
    public class CuentaNotFoundException : BankingException
    {
        public CuentaNotFoundException(string numero) : base("CUENTA_NO_ENCONTRADO", new Exception($"El número de cuenta {numero} no se ha encontrado"))
        {
        }
    }
}
