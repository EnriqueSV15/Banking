using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Exceptions
{
    public class SaldoNoDisponibleException : BankingException
    {
        public SaldoNoDisponibleException(string numero) : base("SALDO_NO_DISPONIBLE", new Exception($"La cuenta {numero} no tiene saldo suficiente"))
        {
        }
    }
}
