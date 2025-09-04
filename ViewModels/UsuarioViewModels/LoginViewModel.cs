using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.UsuarioViewModels
{
    public class LoginViewModel
{
    [Required(ErrorMessage = "O E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail é inválido")] //Valida se o email é valido de forma automatica
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe a senha")]
    public string Password { get; set; }
}
}

