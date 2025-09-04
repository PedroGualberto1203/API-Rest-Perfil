namespace ApiPerfil.Models
{
    public class Venda
    {
        public int VendaID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public IList<VendaItem> VendaItems { get; set; }
        public Usuario Usuario { get; set; }
    }
}