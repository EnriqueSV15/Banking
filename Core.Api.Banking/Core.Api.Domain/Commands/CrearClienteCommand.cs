using Core.Api.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Commands
{
    public class CrearClienteCommand : IRequest<Cliente>
    {
        public required string Nombre { get; set; }
        public required string Genero { get; set; }
        public int Edad {  get; set; }
        public required string Identificacion { get; set; }
        public required string Direccion {  get; set; }
        public required string Telefono { get; set; }  
        public required string Contrasena { get; set; }
                
    }
}
