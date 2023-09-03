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
    public class InsertarMovimientoCommand : IRequest<Movimiento>
    {
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public required string Numero { get; set; }
    }
}
