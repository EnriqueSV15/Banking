using Core.Api.Domain.Enums;
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
    public class CuentaRepository : ICuentaRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        public CuentaRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }
        public async Task<Cuenta> AddAsync(Cuenta cuenta)
        {
            var result = _bankingDbContext.Cuenta.Add(cuenta);
            await _bankingDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Cuenta> GetByNumeroAsync(string Numero)
        {
            return await _bankingDbContext.Cuenta.Where(x => x.Numero == Numero).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Cuenta cuenta)
        {
            _bankingDbContext.Cuenta.Update(cuenta);
            return await _bankingDbContext.SaveChangesAsync();
        }
    }
}
