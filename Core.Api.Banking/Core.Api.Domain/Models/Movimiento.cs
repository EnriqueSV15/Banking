using Core.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Models
{
    public class Movimiento
    {
        public int MovimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }
    }
}
