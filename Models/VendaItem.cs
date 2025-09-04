namespace ApiPerfil.Models
{
    public class VendaItem
    {
        public int VendaItemID { get; set; }
        public int VendaID { get; set; }
        public int ProdutoID { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Venda Venda { get; set; }
        public Produto Produto { get; set; }
    }
}