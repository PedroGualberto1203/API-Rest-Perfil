namespace ApiPerfil.Models
{
    public class Permissao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IList<Usuario> Usuarios { get; set; }
    }
}