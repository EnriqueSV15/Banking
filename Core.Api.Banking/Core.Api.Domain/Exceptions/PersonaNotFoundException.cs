using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Exceptions
{
    public class PersonaNotFoundException : BankingException
    {
        public PersonaNotFoundException(int id) : base("PERSONA_NO_ENCONTRADO", new Exception($"La persona con el id: {id} no se ha encontrado"))
        {
        }
    }
}
