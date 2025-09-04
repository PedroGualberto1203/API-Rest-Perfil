using System.ComponentModel.DataAnnotations;

namespace ApiPerfil.Models.CategoriaViewModels
{
    public class EditorCategoriaViewModel
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo Nome deve ter no mínimo 2 caracteres e no máximo 100")]
        public string Nome { get; set; }
    }
}