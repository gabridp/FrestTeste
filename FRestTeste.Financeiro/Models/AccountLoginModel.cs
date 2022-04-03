using System.ComponentModel.DataAnnotations;

namespace FRestTeste.Financeiro.Models
{
    public class AccountLoginModel
    {
        public string Email { get; set; }

        [MinLength(2, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe sua senha de acesso.")]
        public string Senha { get; set; }
    }
}
