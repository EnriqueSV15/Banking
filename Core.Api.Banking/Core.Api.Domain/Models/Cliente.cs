using Core.Api.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Api.Domain.Models
{
    public class Cliente
    {
        public int ClienteId {  get; set; }
        public required string Contrasena {  get; set; }
        public bool Estado {  get; set; }

        public int PersonaId {  get; set; }

        public Persona Persona { get; set; }

        public ClienteDTO ObtenerDTO()
        {
            return new ClienteDTO {
                Nombre = Persona.Nombre,
                Genero = Persona.Genero,
                Edad = Persona.Edad,
                Identificacion = Persona.Identificacion,
                Direccion = Persona.Direccion,
                Telefono = Persona.Telefono,
                Estado = Estado
            };
        }
    }
}
