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
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("produto");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Dt_Inclusao).HasColumnType("date").IsRequired();
            builder.Property(c => c.Dt_Alteracao).HasColumnType("date");
            builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Descricao).HasMaxLength(300);
            builder.Property(c => c.Preco_Venda).HasPrecision(6,2).IsRequired();
           // builder.HasOne(p => p.Categoria).WithMany(p => p.Produtos).HasForeignKey(p => p.IdCategoria);       
        }
    }
}
