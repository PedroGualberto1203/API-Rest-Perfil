using System.ComponentModel.DataAnnotations;
using ApiPerfil.Attributes;
using ApiPerfil.ViewModels.CategoriaViewModels;

namespace ApiPerfil.ViewModels.ProdutoViewModels
{
    [VerificacaoModelVazio(ErrorMessage = "É necessário informar ao menos um campo para alteração.")]
    public class EditProdutoViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Preco { get; set; }
        public int? CategoriaID { get; set; }
        public CategoriaNomeViewModel? Categoria { get; set; }
    }
}