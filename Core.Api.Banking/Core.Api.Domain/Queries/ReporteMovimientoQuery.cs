using Core.Api.Domain.DTOs;
using Core.Api.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Queries
{
    public class ReporteMovimientoQuery : IRequest<List<ReporteMovimientoDTO>>
    {
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public int ClienteId { get; set; }
    }
}
