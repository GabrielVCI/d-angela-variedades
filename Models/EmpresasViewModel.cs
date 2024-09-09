using d_angela_variedades.Entidades;
using System.ComponentModel.DataAnnotations;

namespace d_angela_variedades.Models
{
    public class EmpresasViewModel
    {
        [Required(ErrorMessage = "Nombre requerido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        ErrorMessage = "Solo letras")]
        public string NombreAdministrador { get; set; }

        [Required(ErrorMessage = "Apellido requerido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        ErrorMessage = "Solo letras")]
        public string ApellidoAdministrador { get; set; }

        [Required(ErrorMessage = "Nombre de empresa requerido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        ErrorMessage = "Solo letras")]
        public string NombreEmpresa { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Contraseña requerida")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Password {  get; set; }

        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Required(ErrorMessage = "Contraseña requerida")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
        public IFormFile? LogoEmpresa { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Correo requerido")]
        [StringLength(40, ErrorMessage = "Máximo 20 caracteres")]
        public string EmailAdministrador { get; set; }

        public string? LogoURL { get; set; }
    }
}
