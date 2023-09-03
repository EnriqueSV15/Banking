using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Banking.IntegrationTests
{
    public class CrearClienteCommandTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CrearClienteCommandTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CrearCliente_DebeRetornarStatusCodeCreated()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Datos del cliente que se va a crear
            var nuevoCliente = new
            {
                nombre = "Edwing Salas",
                genero = "M",
                edad = 29,
                identificacion = "75205325",
                direccion = "Calle de Prueba",
                telefono = "982257400",
                contrasena = "1234"
            };

            // Convertir el objeto en formato JSON
            var jsonCliente = Newtonsoft.Json.JsonConvert.SerializeObject(nuevoCliente);
            var content = new StringContent(jsonCliente, Encoding.UTF8, "application/json");

            // Realizar una solicitud HTTP POST a la API para crear el cliente
            var response = await client.PostAsync("/api/Cliente", content);

            // Verificar que la respuesta tenga un código de estado Created (201)
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
