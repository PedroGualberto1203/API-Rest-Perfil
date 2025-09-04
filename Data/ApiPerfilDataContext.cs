using ApiPerfil.Data.Mappings;
using ApiPerfil.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPerfil.Data
{
    public class ApiPerfilDataContext : DbContext
    {
        public ApiPerfilDataContext(DbContextOptions<ApiPerfilDataContext> options) : base(options)
        {

        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItems { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new PermissaoMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new VendaItemMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
        }
    }
}