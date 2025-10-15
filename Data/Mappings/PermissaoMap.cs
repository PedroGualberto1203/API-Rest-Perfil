using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class PermissaoMap : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.ToTable("Permissoes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50); 

            builder.HasIndex(x => x.Nome, "IX_Permissao_Nome").IsUnique();

            // relacionamento Muitos-para-Muitos
            builder
                .HasMany(x => x.Usuarios)
                .WithMany(x => x.Permissoes)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioPermissoes", // Nome da tabela de junção
                    usuario => usuario
                        .HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_UsuarioPermissoes_UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade),
                    permissao => permissao
                        .HasOne<Permissao>()
                        .WithMany()
                        .HasForeignKey("PermissaoId")
                        .HasConstraintName("FK_UsuarioPermissoes_PermissaoId")
                        .OnDelete(DeleteBehavior.Cascade));

            builder.HasData(
                new Permissao { Id = 1, Nome = "Admin" },
                new Permissao { Id = 2, Nome = "UsuarioPadrao" },
                new Permissao { Id = 3, Nome = "GerenteDeEstoque" }
            );
            
            builder
            .HasMany(x => x.Usuarios)
            .WithMany(x => x.Permissoes)
            .UsingEntity<Dictionary<string, object>>(
                "UsuarioPermissoes",
                usuario => usuario.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId"),
                permissao => permissao.HasOne<Permissao>().WithMany().HasForeignKey("PermissaoId"),
                j => j.HasData(new { UsuarioId = 1, PermissaoId = 1 }));
        }
    }
}