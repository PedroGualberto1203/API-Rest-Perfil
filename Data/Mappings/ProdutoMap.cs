using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasColumnName("Quantidade")
                .HasColumnType("INT")
                .HasDefaultValue(0);

            builder.Property(x => x.Preco)
                .IsRequired()
                .HasColumnName("Preco")
                .HasColumnType("DECIMAL(10, 2)");
            
            // Relacionamento com Categoria
            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.CategoriaID)
                .HasConstraintName("FK_Produtos_Categorias")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasCheckConstraint("CHK_Produtos_Quantidade", "[Quantidade] >= 0");
            builder.HasCheckConstraint("CHK_Produtos_Preco", "[Preco] >= 0");
        }
    }
}