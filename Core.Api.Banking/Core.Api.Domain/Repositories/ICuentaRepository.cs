using Core.Api.Domain.Enums;
using Core.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Repositories
{
    public interface ICuentaRepository
    {
        public Task<Cuenta> AddAsync(Cuenta cuenta);
        public Task<int> UpdateAsync(Cuenta cuenta);
        public Task<Cuenta> GetByNumeroAsync(string numero);
    }
}
