using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Exceptions
{
    public class ClienteNotFoundException : BankingException
    {
        public ClienteNotFoundException(int id) : base("CLIENTE_NO_ENCONTRADO", new Exception($"El Cliente con el id: {id} no se ha encontrado"))
        {
        }
    }
}
