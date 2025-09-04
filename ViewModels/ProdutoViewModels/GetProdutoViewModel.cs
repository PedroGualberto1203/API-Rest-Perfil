using System.ComponentModel.DataAnnotations;
using ApiPerfil.ViewModels.CategoriaViewModels;

namespace ApiPerfil.ViewModels.ProdutoViewModels
{
    public class GetProdutoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O campo Nome deve ter no mínimo 2 caracteres e no máximo 150")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório")]
        public decimal Preco { get; set; }
        public CategoriaNomeViewModel Categoria { get; set; }
    }
}