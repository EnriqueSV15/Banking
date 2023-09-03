using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.DTOs
{
    public class ReporteMovimientoDTO
    {
        public required string Fecha { get; set; }
        public required string Cliente { get; set; }
        public required string Numero { get; set; }
        public required string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public required string Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
