using System.ComponentModel.DataAnnotations;

namespace d_angela_variedades.Models
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmar contraseña requerida")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string  Token { get; set; }
    }
}
