using Core.Api.Domain.Models;
using Core.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Data.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        public MovimientoRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }
        public async Task<Movimiento> AddAsync(Movimiento movimiento)
        {
            var result = _bankingDbContext.Movimiento.Add(movimiento);
            await _bankingDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<Movimiento>> ObtenerMovimientosPorCuentaAsync(int cuentaId, DateTime fecha)
        {

            var result = _bankingDbContext.Movimiento.Where(x => x.CuentaId == cuentaId && x.Fecha.Date == fecha.Date);

            return await result.ToListAsync();
        }

        public async Task<List<Movimiento>> ObtenerMovimientosPorRangoFechasAsync(DateTime fechaIni, DateTime fechaFin, int clienteId)
        {
            //var result = _bankingDbContext.Movimiento.Where(x => x.Fecha.Date >= fechaIni && x.Fecha.Date <= fechaFin.Date);

            var result = _bankingDbContext.Movimiento
                .Include(x => x.Cuenta)
                .ThenInclude(c => c.Cliente)
                .ThenInclude(d => d.Persona)
                .Where(x => x.Fecha.Date >= fechaIni && x.Fecha.Date <= fechaFin.Date && x.Cuenta.ClienteId == clienteId);

            return await result.ToListAsync();
        }
    }
}
