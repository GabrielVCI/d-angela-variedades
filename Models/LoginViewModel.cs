using System.ComponentModel.DataAnnotations;

namespace d_angela_variedades.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Correo requerido")]
        [StringLength(40, ErrorMessage = "Maximo 30 caracteres")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [StringLength(20, ErrorMessage = "Maximo 30 caracteres")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
