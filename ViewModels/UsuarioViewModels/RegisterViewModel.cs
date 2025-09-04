using System.ComponentModel.DataAnnotations;
using ApiPerfil.Models;

namespace ApiPerfil.ViewModels.UsuarioViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail é inválido")] //Valida se o email é valido de forma automatica
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF { get; set; }
        public string? SenhaHash { get; set; }
        public List<Permissao>? Permissoes { get; set; } = new List<Permissao>();
    }
}

