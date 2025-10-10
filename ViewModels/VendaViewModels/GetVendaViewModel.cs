namespace ApiPerfil.ViewModels.VendaViewModels
{
    public class GetVendaViewModel
    {
        public int VendaId { get; set; }
        public int UsuarioID { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public List<BaseGetVendaViewModel> VendaItems { get; set; }
    }
}