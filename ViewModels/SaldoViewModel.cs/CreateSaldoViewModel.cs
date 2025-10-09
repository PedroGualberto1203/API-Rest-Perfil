using System.ComponentModel.DataAnnotations;

namespace ApiPerfil.ViewModels.SaldoViewModel
{
    public class CreateSaldoViewModel
    {
        [Required(ErrorMessage = "O campo Saldo é obrigatório")]
        public decimal Saldo { get; set; }
        
    }
}