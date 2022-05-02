using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoWK.Data.Entity.MySQL.Mapeamento;
using TesteTecnicoWK.Dominio.Entidades;

namespace TesteTecnicoWK.Data.Entity.MySQL.Contexto
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<Categoria>? Categorias { get; set;}
        public DbSet<Produto>? Produtos { get; set;}

        public MySqlDbContext()
        {
            
        }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) 
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //   new ProdutoMap().Configure(modelBuilder.Entity<Produto>());
            //  new CategoriaMap().Configure(modelBuilder.Entity<Categoria>());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());

        }
    }
}
