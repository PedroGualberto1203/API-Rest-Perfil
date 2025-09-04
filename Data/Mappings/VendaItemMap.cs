using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class VendaItemMap : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            //Tabela
            builder.ToTable("VendaItens");

            // Mostrei qual é a chave primária
            builder.HasKey(x => x.VendaItemID);

            builder.Property(x => x.VendaItemID)
                .ValueGeneratedOnAdd() // Auto Incremento
                .UseIdentityColumn(); // Adiciona de um 1 em 1

            //Propriedades
            builder.Property(x => x.VendaID)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("VendaID") // Nome da coluna a ser gerada
                .HasColumnType("INT"); // Tipo

            builder.Property(x => x.ProdutoID)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("ProdutoID") // Nome da coluna a ser gerada
                .HasColumnType("INT"); // Tipo

            builder.Property(x => x.Quantidade)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("Quantidade") // Nome da coluna a ser gerada
                .HasColumnType("INT"); // Tipo

            builder.Property(x => x.PrecoUnitario)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("PrecoUnitario") // Nome da coluna a ser gerada
                .HasColumnType("DECIMAL(10, 2)"); // Tipo

            //Índices(usados para procura/busca)
            builder.HasIndex(x => x.VendaItemID, "IX_VendaItem_VendaItemID") // Propriedade que vai receber o index e o nome do index
                .IsUnique();//Para garantir que o index seja unico

            //Relacionamento Um para Muitos(uma venda tem muitos ItemVenda)
            builder.HasOne(x => x.Venda)
            .WithMany(x => x.VendaItems)
            .HasConstraintName("FK_Venda_VendaItem");

            //Relacionamento Um para Muitos(um produto pode ter muitos ItemVenda)
            builder.HasOne(x => x.Produto)
            .WithMany(x => x.VendaItems)
            .HasConstraintName("FK_Produto_VendaItem");

        }
    }
}