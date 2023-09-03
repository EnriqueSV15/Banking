using Core.Api.Domain.Commands;
using Core.Api.Domain.Exceptions;
using Core.Api.Domain.Handlers;
using Core.Api.Domain.Models;
using Core.Api.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;

namespace Core.Api.Domain.Tests
{
    public class ActualizarClienteHandlerTests
    {
        private readonly ActualizarClienteHandler _actualizarClienteHandler;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly Mock<IValidator<ActualizarClienteCommand>> _validatorMock;
        private readonly Mock<ILogger<ActualizarClienteHandler>> _loggerMock;
        public ActualizarClienteHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _personaRepositoryMock = new Mock<IPersonaRepository>();
            _validatorMock = new Mock<IValidator<ActualizarClienteCommand>>();
            _loggerMock = new Mock<ILogger<ActualizarClienteHandler>>();

            _validatorMock
            .Setup(validator => validator.ValidateAsync(It.IsAny<ActualizarClienteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult { });

            _actualizarClienteHandler = new ActualizarClienteHandler(_personaRepositoryMock.Object,
                _clienteRepositoryMock.Object,
                _validatorMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ClienteNoEncontrado_LanzarClienteNotFoundException()
        {
            //Arrange
            var request = new ActualizarClienteCommand
            {
                Nombre = "Edwing Salas",
                Genero = "M",
                Edad = 29,
                Identificacion = "75205325",
                Direccion = "Calle 20A 162",
                Telefono = "982257403",
                Estado = true
            };

            //Assert
            await Assert.ThrowsAsync<ClienteNotFoundException>(() => _actualizarClienteHandler.Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_ClienteNoEncontrado_LanzarPersonaNotFoundException()
        {
            //Arrange
            var request = new ActualizarClienteCommand
            {
                Nombre = "Edwing Salas",
                Genero = "M",
                Edad = 29,
                Identificacion = "75205325",
                Direccion = "Calle 20A 162",
                Telefono = "982257403",
                Estado = true
            };

            _clienteRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(new Cliente
              {
                  ClienteId = 5,
                  Contrasena = "xxxx",
                  Estado = true,
                  PersonaId = 5
              });

            //Assert
            await Assert.ThrowsAsync<PersonaNotFoundException>(() => _actualizarClienteHandler.Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_DatosClientesValidos_RetornarTrue()
        {
            //Arrange
            var request = new ActualizarClienteCommand
            {
                Nombre = "Edwing Salas",
                Genero = "M",
                Edad = 29,
                Identificacion = "75205325",
                Direccion = "Calle 20A 162",
                Telefono = "982257403",
                Estado = true
            };

            _clienteRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(new Cliente
              {
                  ClienteId = 5,
                  Contrasena = "xxxx",
                  Estado = true,
                  PersonaId = 5
              });

            _personaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(new Persona
              {
                  Nombre = "Cesar Braco",
                  Edad = 5,
                  Genero = "M",
                  Direccion = "Calle Los Gavilanes",
                  Identificacion = "NN",
                  Telefono = "5555555"
              });

            _clienteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Cliente>()))
              .ReturnsAsync(5);

            _personaRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Persona>()))
              .ReturnsAsync(5);
            //Act
            var result = await _actualizarClienteHandler.Handle(request, new CancellationToken());
            //Assert
            Assert.True(result);
        }
    }
}