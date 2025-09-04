namespace ApiPerfil.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public decimal Saldo { get; set; }
        public List<Permissao> Permissoes { get; set; } = new List<Permissao>();
        public IList<Venda> Vendas { get; set; }
    }
}