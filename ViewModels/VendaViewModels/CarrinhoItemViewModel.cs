using System.ComponentModel.DataAnnotations;

namespace ApiPerfil.ViewModels.VendaViewModels
{
    public class CarrinhoItemViewModel
    {
        [Required(ErrorMessage = "O campo ProdutoId é obrigatório")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
        public int Quantidade { get; set; }
    }
}