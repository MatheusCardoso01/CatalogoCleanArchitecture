using Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Infrastructure.EntitiesConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    { 
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Descricao).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Preco).HasPrecision(10, 2);
        builder.Property(p => p.Imagemurl).HasMaxLength(250);
        builder.Property(p => p.Estoque).IsRequired().HasDefaultValue(1);
        builder.Property(p => p.DataCadastro).IsRequired();

        builder.HasOne(e => e.Categoria).WithMany(c => c.Produtos).HasForeignKey(e => e.CategoriaId);
    }
}
