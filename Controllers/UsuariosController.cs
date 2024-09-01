using d_angela_variedades.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext applicationDbContext;

        public UsuariosController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext applicationDbContext) 
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro() 
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Registro(EmpresasViewModel empresa)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(empresa);
        //    }

        //    var empresaExiste = await applicationDbContext.Empresas.FirstOrDefaultAsync(emp => emp.NombreEmpresa == empresa.NombreEmpresa);

        //    if (empresaExiste is not null)
        //    {
        //        ModelState.AddModelError("NombreEmpresa", "Esta empresa ya existe");
        //        return View(empresaExiste);
        //    }

        //    var usuarioExiste = await applicationDbContext.Users.FirstOrDefaultAsync(user => user.Email == empresa.EmailAdministrador);

        //    if(usuarioExiste is not null)
        //    {
        //        ModelState.AddModelError("EmailAdministrador", "Este correo ya está registrado");
        //        return View(empresaExiste);
        //    }

        //    var 
        //}
    }
}
