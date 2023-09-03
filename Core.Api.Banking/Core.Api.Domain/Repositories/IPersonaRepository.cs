using Core.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Repositories
{
    public interface IPersonaRepository
    {
        public Task<List<Persona>> GetListAsync();
        public Task<Persona> GetByIdAsync(int Id);
        public Task<Persona> AddAsync(Persona persona);
        public Task<int> UpdateAsync(Persona persona);
        public Task<int> DeleteAsync(int Id);
    }
}
