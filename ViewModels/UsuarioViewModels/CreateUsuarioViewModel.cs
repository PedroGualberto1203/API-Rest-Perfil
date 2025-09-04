using System.ComponentModel.DataAnnotations;
using ApiPerfil.ViewModels;
using ApiPerfil.ViewModels.PermissaoViewModels;

namespace ApiPerfil.Models.UsuarioViewModels
{
    public class CreateUsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo Nome deve ter no mínimo 2 caracteres e no máximo 100")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        public string Email { get; set; }

        public IList<PermissaoViewModel> Permissoes { get; set; }
    }
}