using Core.Api.Domain.Enums;
using Core.Api.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Commands
{
    public class CrearCuentaCommand : IRequest<Cuenta>
    {
        public required string Numero { get; set; }
        public TipoCuenta Tipo { get; set; }
        public decimal Saldo { get; set; }
        public int ClienteId { get; set; }
    }
}
