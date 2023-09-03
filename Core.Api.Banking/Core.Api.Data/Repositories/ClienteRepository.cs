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
    public class ClienteRepository : IClienteRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        public ClienteRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }
        public async Task<Cliente> AddAsync(Cliente clientes)
        {
            var result = _bankingDbContext.Cliente.Add(clientes);
            await _bankingDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<int> DeleteAsync(int Id)
        {
            var filteredData = _bankingDbContext.Cliente.Where(x => x.ClienteId == Id).FirstOrDefault();
            _bankingDbContext.Cliente.Remove(filteredData);

            return await _bankingDbContext.SaveChangesAsync();
        }

        public async Task<Cliente> GetByIdAsync(int Id)
        {
            return await _bankingDbContext.Cliente.Where(x => x.ClienteId == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Cliente>> GetListAsync()
        {
            return await _bankingDbContext.Cliente.ToListAsync();
        }

        public async Task<int> UpdateAsync(Cliente clientes)
        {
            _bankingDbContext.Cliente.Update(clientes);
            return await _bankingDbContext.SaveChangesAsync();            
        }
    }
}
