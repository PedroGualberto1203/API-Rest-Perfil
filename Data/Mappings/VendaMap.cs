using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Vendas");
            builder.HasKey(x => x.VendaID);
            builder.Property(x => x.VendaID).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.DataVenda)
                .IsRequired()
                .HasColumnName("DataVenda")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()"); // Corresponde ao SYSDATETIME() do SQL

            builder.Property(x => x.ValorTotal)
                .IsRequired()
                .HasColumnName("ValorTotal")
                .HasColumnType("DECIMAL(10, 2)");
            
            // Relacionamento com Usuario
            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Vendas)
                .HasForeignKey(x => x.UsuarioID)
                .HasConstraintName("FK_Vendas_Usuarios")
                .OnDelete(DeleteBehavior.NoAction);
            
            // CHECK Constraint
            builder.HasCheckConstraint("CHK_Vendas_ValorTotal", "[ValorTotal] >= 0");
        }
    }
}