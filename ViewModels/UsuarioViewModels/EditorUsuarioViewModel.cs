using System.ComponentModel.DataAnnotations;
using ApiPerfil.ViewModels;
using ApiPerfil.ViewModels.PermissaoViewModels;

namespace ApiPerfil.Models.UsuarioViewModels
{
    public class EditorUsuarioViewModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo NomeCompleto deve ter no mínimo 2 caracteres e no máximo 100")]
        public string? NomeCompleto { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "O campo Telefone deve ter no mínimo 11 e máximo 20 caracteres e no máximo 100")]
        public string? Telefone { get; set; }

        [EmailAddress(ErrorMessage = "O e-mail é inválido")] //Valida se o email é valido de forma automatica
        public string? Email { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo CPF deve ter no mínimo e máximo 11 caracteres")]
        public string? CPF { get; set; }
        public IList<PermissaoViewModel>? Permissoes { get; set; }
    }
}