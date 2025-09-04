using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Tabela
            builder.ToTable("Usuarios");

            // Mostrei qual é a chave primária
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd() // Auto Incremento
                .UseIdentityColumn(); // Adiciona de um 1 em 1

            //Propriedades
            builder.Property(x => x.NomeCompleto)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("NomeCompleto") // Nome da coluna a ser gerada
                .HasColumnType("NVARCHAR") // Tipo
                .HasMaxLength(200); //Tamanho

            builder.Property(x => x.Telefone)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("Telefone") // Nome da coluna a ser gerada
                .HasColumnType("VARCHAR") // Tipo
                .HasMaxLength(20); //Tamanho

            builder.Property(x => x.CPF)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("CPF") // Nome da coluna a ser gerada
                .HasColumnType("VARCHAR") // Tipo
                .HasMaxLength(11); //Tamanho

            builder.Property(x => x.Email)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("Email") // Nome da coluna a ser gerada
                .HasColumnType("VARCHAR") // Tipo
                .HasMaxLength(255); //Tamanho

            builder.Property(x => x.SenhaHash)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("SenhaHash") // Nome da coluna a ser gerada
                .HasColumnType("VARCHAR") // Tipo
                .HasMaxLength(255); //Tamanho

            builder.Property(x => x.Saldo)
                .IsRequired() //Gera o NOT NULL
                .HasColumnName("Saldo") // Nome da coluna a ser gerada
                .HasColumnType("DECIMAL(10, 2)"); // Tipo

            //Índices(usados para procura/busca)
            builder.HasIndex(x => x.NomeCompleto, "IX_User_NomeCompleto") // Propriedade que vai receber o index e o nome do index
                .IsUnique();//Para garantir que o index seja unico
            
        }
    }
}