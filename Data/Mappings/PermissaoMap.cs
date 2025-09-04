using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPerfil.Data.Mappings
{
    public class PermissaoMap : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            //Tabela
            builder.ToTable("Permissoes");

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
                .HasMaxLength(50); //Tamanho

            //Índices(usados para procura/busca)
            builder.HasIndex(x => x.Nome, "IX_Permissao_Nome") // Propriedade que vai receber o index e o nome do index
                .IsUnique();//Para garantir que o index seja unico

            //Relacionamento Muito para Muitos(Um Usuário pode ter muitas permissoes e o contrário tbm)
            builder.HasMany(x => x.Usuarios) //Tem muitos usuários
            .WithMany(x => x.Permissoes) //Com muitas permissoes
            .UsingEntity<Dictionary<string, object>>( // Gera uma tabela virtual, baseada em um Dictionary(uma lista), que recebe uma string e um objeto
                "UsuarioPermissoes", // a string
                post => post.HasOne<Usuario>() //User e Permissao sao os objetos, o objeto é dividido em dois. // Uma entrada na tabela UsuarioPermissoes tem uma relação com a tabela User  
                    .WithMany() // o Usuario tem muitas permissoes
                    .HasForeignKey("UsuarioId") //Usa a chave estrangeira "UsuarioId" que aponta para o usuario
                    .HasConstraintName("FK_UsuarioPermissoes_UsuarioId")
                    .OnDelete(DeleteBehavior.Cascade),
                tag => tag.HasOne<Permissao>() // O usuario tem uma permissao // Inversamente, essa entrada também tem uma relação com um usuario.
                    .WithMany() // Essa permissao tem muitos usuarios
                    .HasForeignKey("PermissaoId") //Usa a chave estrangeira "TagId".
                    .HasConstraintName("FK_UsuarioPermissoes_PermissaoId")
                    .OnDelete(DeleteBehavior.Cascade)
            ); // Ou seja, gerou uma tabela cahamda UsuarioPermissoes com duas colunas, UsuarioId e PermissaoId
            
        }
    }
}