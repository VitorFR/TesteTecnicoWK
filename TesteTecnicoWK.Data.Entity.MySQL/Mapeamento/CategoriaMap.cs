using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoWK.Dominio.Entidades;

namespace TesteTecnicoWK.Data.Entity.MySQL.Mapeamento
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categoria");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Dt_Inclusao).HasColumnType("date").IsRequired();
            builder.Property(c => c.Dt_Alteracao).HasColumnType("date");
            builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
        }
    }
}
