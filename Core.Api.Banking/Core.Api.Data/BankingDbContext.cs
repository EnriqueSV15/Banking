using Core.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Data
{
    public class BankingDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public BankingDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Movimiento> Movimiento { get; set; }
    }
}
