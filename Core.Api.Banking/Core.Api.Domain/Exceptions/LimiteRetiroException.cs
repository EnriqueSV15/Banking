using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Exceptions
{
    public class LimiteRetiroException : BankingException
    {
        public LimiteRetiroException() : base("CUPO_DIARIO_EXCEDIDO", new Exception("Cupo diario excedido"))
        {
        }
    }
}
