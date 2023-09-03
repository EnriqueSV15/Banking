using Core.Api.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Queries
{
    public class ObtenerClientePorIdQuery : IRequest<Cliente>
    {
        public int Id { get; set; }
    }
}
