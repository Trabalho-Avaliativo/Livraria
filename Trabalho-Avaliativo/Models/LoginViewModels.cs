using System.ComponentModel.DataAnnotations;

namespace Trabalho_Avaliativo.Models
{
    public class LoginViewModels
    {

        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

    }
}
