using Core.Api.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Commands
{
    public class EliminarClienteCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
