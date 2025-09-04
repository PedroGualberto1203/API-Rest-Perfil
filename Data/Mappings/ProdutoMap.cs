using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            //Tabela
            builder.ToTable("Produtos");

            // Mostrei qual é a chave primária
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd() // Auto Incremento
                .UseIdentityColumn(); // Adiciona de um 1 em 1

            //Propriedades
            builder.Property(x => x.Nome)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("Nome") // Nome da coluna a ser gerada
                .HasColumnType("NVARCHAR") // Tipo
                .HasMaxLength(150); //Tamanho

            builder.Property(x => x.Quantidade)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("Quantidade") // Nome da coluna a ser gerada
                .HasColumnType("INT"); // Tipo

            builder.Property(x => x.Preco)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("Preco") // Nome da coluna a ser gerada
                .HasColumnType("DECIMAL(10, 2)"); // Tipo

            builder.Property(x => x.CategoriaID)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("CategoriaID") // Nome da coluna a ser gerada
                .HasColumnType("INT"); // Tipo

            //Índices(usados para procura/busca)
            builder.HasIndex(x => x.Nome, "IX_Produto_Nome") // Propriedade que vai receber o index e o nome do index
                .IsUnique();//Para garantir que o index seja unico

            //Relacionamento Um para Muitos(uma categoria tem muitos produtos)
            builder.HasOne(x => x.Categoria)
            .WithMany(x => x.Produtos)
            .HasConstraintName("FK_Categoria_Produto");
        }
    }
}