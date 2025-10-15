using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class VendaItemMap : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            builder.ToTable("VendaItens");
            builder.HasKey(x => x.VendaItemID);
            builder.Property(x => x.VendaItemID).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasColumnName("Quantidade")
                .HasColumnType("INT");

            builder.Property(x => x.PrecoUnitario)
                .IsRequired()
                .HasColumnName("PrecoUnitario")
                .HasColumnType("DECIMAL(10, 2)");
            
            // Relacionamentos (já estavam corretos, apenas confirmando)
            builder.HasOne(x => x.Venda)
                .WithMany(x => x.VendaItems)
                .HasForeignKey(x => x.VendaID)
                .HasConstraintName("FK_VendaItens_Vendas")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.VendaItems)
                .HasForeignKey(x => x.ProdutoID)
                .HasConstraintName("FK_VendaItens_Produtos")
                .OnDelete(DeleteBehavior.NoAction);
            
            // CHECK Constraints
            builder.HasCheckConstraint("CHK_VendaItens_Quantidade", "[Quantidade] > 0");
            builder.HasCheckConstraint("CHK_VendaItens_PrecoUnitario", "[PrecoUnitario] >= 0");

            // UNIQUE Constraint composta
            builder.HasIndex(vi => new { vi.VendaID, vi.ProdutoID })
                   .IsUnique()
                   .HasDatabaseName("UQ_Venda_Produto");
        }
    }
}