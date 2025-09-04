using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //Tabela
            builder.ToTable("Categorias");

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
                .HasMaxLength(100); //Tamanho

            //Índices(usados para procura/busca)
            builder.HasIndex(x => x.Nome, "IX_Categoria_Nome") // Propriedade que vai receber o index e o nome do index
                .IsUnique();//Para garantir que o index seja unico

        }
    }
}