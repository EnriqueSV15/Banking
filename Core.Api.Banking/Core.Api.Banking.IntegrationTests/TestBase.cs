using Core.Api.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Banking.IntegrationTests
{
    public class TestBase
    {
        protected ApiWebApplicationFactory Application;

        public async Task<HttpClient> CreateTestUser()
        {
            using var scope = Application.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var client = Application.CreateClient();

            return client;
        }

        [TearDown]
        public async Task Down()
        {
            await ResetState();
        }

        /// <summary>
        /// Crea un HttpClient incluyendo un JWT válido con usuario default
        /// </summary>
        public Task<HttpClient> GetClientTestAsync() =>
            CreateTestUser();

        /// <summary>
        /// Libera recursos al terminar todas las pruebas
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            Application.Dispose();
        }

        /// <summary>
        /// Inicializa la API y la BD antes de iniciar las pruebas
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Application = new ApiWebApplicationFactory();

            EnsureDatabase();
        }

        /// <summary>
        /// Shortcut para ejecutar IRequests con el Mediador
        /// </summary>
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = Application.Services.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        /// <summary>
        /// Shortcut para agregar Entities a la BD
        /// </summary>
        protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = Application.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<BankingDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Shortcut para buscar entities por primary key
        /// </summary>
        protected async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            using var scope = Application.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<BankingDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        /// <summary>
        /// Shortcut para buscar entities según un criterio
        /// </summary>
        protected async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            using var scope = Application.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<BankingDbContext>();

            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Se asegura de crear la BD
        /// </summary>
        private void EnsureDatabase()
        {
            using var scope = Application.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<BankingDbContext>();

            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Se asegura de limpiar la BD
        /// </summary>
        /// <returns></returns>
        private async Task ResetState()
        {
            using var scope = Application.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<BankingDbContext>();
            // Aquí en un caso real, podemos usar Respawn para reiniciar la BD en lugar
            // de eliminar tablas y recrearlas
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //await MyAppDbContextSeed.SeedDataAsync(context);
        }
    }
}
