namespace ApiPerfil.ViewModels.VendaViewModels
{
    public class ReturnVendaViewModel
    {
        public int VendaId { get; set; }
        public int UsuarioID { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public IList<VendaItemViewModel> VendaItems { get; set; }
    }
}