using System.ComponentModel.DataAnnotations;

namespace d_angela_variedades.Models
{
    public class ForgotPasswordViewMode
    {
        [EmailAddress(ErrorMessage = "Necesita ser una dirección de correo válida")]
        [Display(Name = "Ingresa tu correo electrónico")]
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        public string Correo { get; set; }
    }
}
