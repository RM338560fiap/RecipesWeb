using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Receitas> Receitas { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Login> Logins { get; set; }

    }
}
