using Core.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Repositories
{
    public interface IMovimientoRepository
    {
        public Task<Movimiento> AddAsync(Movimiento movimiento);
        public Task<List<Movimiento>> ObtenerMovimientosPorCuentaAsync(int cuentaId, DateTime fecha);
        public Task<List<Movimiento>> ObtenerMovimientosPorRangoFechasAsync(DateTime fechaIni, DateTime fechaFin, int clienteId);
    }
}
