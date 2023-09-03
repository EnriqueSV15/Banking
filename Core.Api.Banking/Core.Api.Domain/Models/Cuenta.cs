using Core.Api.Domain.DTOs;
using Core.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Models
{
    public class Cuenta
    {
        public int CuentaId { get; set; }
        public required string Numero { get; set; }
        public TipoCuenta Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

    }
}
