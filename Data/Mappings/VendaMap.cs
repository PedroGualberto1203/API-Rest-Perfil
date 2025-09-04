using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            //Tabela
            builder.ToTable("Vendas");

            // Mostrei qual é a chave primária
            builder.HasKey(x => x.VendaID);

            builder.Property(x => x.VendaID)
                .ValueGeneratedOnAdd() // Auto Incremento
                .UseIdentityColumn(); // Adiciona de um 1 em 1

            //Propriedades
            builder.Property(x => x.UsuarioID)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("UsuarioID") // Nome da coluna a ser gerada
                .HasColumnType("INT"); // Tipo

            builder.Property(x => x.DataVenda)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("DataVenda") // Nome da coluna a ser gerada
                .HasColumnType("DATETIME"); // Tipo

            builder.Property(x => x.ValorTotal)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("ValorTotal") // Nome da coluna a ser gerada
                .HasColumnType("DECIMAL(10, 2)"); // Tipo

            //Índices(usados para procura/busca)
            builder.HasIndex(x => x.VendaID, "IX_Venda_VendaID") // Propriedade que vai receber o index e o nome do index
                .IsUnique();//Para garantir que o index seja unico

            //Relacionamento Um para Muitos(um usuario tem muitas vendas)
            builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Vendas)
            .HasConstraintName("FK_Usuario_Venda");
        }
    }
}