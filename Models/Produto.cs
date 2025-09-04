namespace ApiPerfil.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; } //Prop de navegação
        public IList<VendaItem> VendaItems { get; set; }
    }
}