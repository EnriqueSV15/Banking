using Core.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Repositories
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>> GetListAsync();
        public Task<Cliente> GetByIdAsync(int Id);
        public Task<Cliente> AddAsync(Cliente clientes);
        public Task<int> UpdateAsync(Cliente clientes);
        public Task<int> DeleteAsync(int Id);
    }
}
