using Core.Api.Banking.Middleware;
using Core.Api.Data;
using Core.Api.Data.Repositories;
using Core.Api.Domain;
using Core.Api.Domain.Queries;
using Core.Api.Domain.Repositories;
using Core.Api.Domain.Validators;
using FluentValidation;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.AddValidatorsFromAssemblyContaining<ReporteMovimientoValidator>(ServiceLifetime.Transient);
// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ObtenerClientePorIdQuery).Assembly));
builder.Services.AddDbContext<BankingDbContext>();

builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddTransient<ICuentaRepository, CuentaRepository>();
builder.Services.AddTransient<IMovimientoRepository, MovimientoRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCustomExeptionHandler();

app.Run();

public partial class Program { }