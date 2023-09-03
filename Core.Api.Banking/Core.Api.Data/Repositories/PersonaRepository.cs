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
    public class PersonaRepository : IPersonaRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        public PersonaRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }
        public async Task<Persona> AddAsync(Persona persona)
        {
            var result = _bankingDbContext.Persona.Add(persona);
            await _bankingDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<int> DeleteAsync(int Id)
        {
            var filteredData = _bankingDbContext.Persona.Where(x => x.PersonaId == Id).FirstOrDefault();
            _bankingDbContext.Persona.Remove(filteredData);

            return await _bankingDbContext.SaveChangesAsync();
        }

        public async Task<Persona> GetByIdAsync(int Id)
        {
            return await _bankingDbContext.Persona.Where(x => x.PersonaId == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Persona>> GetListAsync()
        {
            return await _bankingDbContext.Persona.ToListAsync();
        }

        public async Task<int> UpdateAsync(Persona persona)
        {
            _bankingDbContext.Persona.Update(persona);
            return await _bankingDbContext.SaveChangesAsync();
        }
    }
}
