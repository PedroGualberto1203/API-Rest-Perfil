using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasColumnName("NomeCompleto")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Telefone)
                .IsRequired(false) 
                .HasColumnName("Telefone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.SenhaHash)
                .IsRequired()
                .HasColumnName("SenhaHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Saldo)
                .IsRequired()
                .HasColumnName("Saldo")
                .HasColumnType("DECIMAL(10, 2)")
                .HasDefaultValue(0.00);

            builder.HasIndex(x => x.Email, "IX_User_Email").IsUnique();
            builder.HasIndex(x => x.CPF, "IX_User_CPF").IsUnique();

            builder.HasCheckConstraint("CHK_Usuarios_Saldo", "[Saldo] >= 0");


            builder.HasData(new Usuario
            {
                Id = 1,
                NomeCompleto = "Admin Master",
                Email = "admin@email.com",
                CPF = "00000000000",
                Telefone = "999999999",
                Saldo = 500,
                SenhaHash = "10000.T+mynenJ2vkjFAjTMWvJkg==.fcN+tJH6rWsSjuvGbHlcCjaQoVEspNSczr0pwsX8v80="
            });
}
        
    }
}