using Core.Api.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Commands
{
    public class ActualizarCuentaCommand : IRequest<bool>
    {
        public required string Numero { get; set; }
        public TipoCuenta Tipo { get; set; }
        public bool Estado { get; set; }
    }
}
