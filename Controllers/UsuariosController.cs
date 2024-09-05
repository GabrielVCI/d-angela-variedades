using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace d_angela_variedades.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IEmpresasRepositorio empresasRepositorio;
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string Logos = "LogoEmpresa";


        public UsuariosController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ApplicationDbContext applicationDbContext, IAlmacenadorArchivos almacenadorArchivos,
            IUsuariosRepositorio usuariosRepositorio, IEmpresasRepositorio empresasRepositorio, IServiciosUsuarios serviciosUsuarios) 
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
            this.usuariosRepositorio = usuariosRepositorio;
            this.empresasRepositorio = empresasRepositorio;
            this.serviciosUsuarios = serviciosUsuarios;
            this.almacenadorArchivos = almacenadorArchivos;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(EmpresasViewModel empresa)
        {

            //Variable para asignarle el resultado del almacenamiento del logo, que tiene el tipo de la clase AlmacenadorArchivosResultado
            var UrlFoto = new AlmacenadorArchivosResultado();

            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            //Variable para verificar si el nombre de la empresa ya fue registrado
            var empresaExiste = await applicationDbContext.Empresas.FirstOrDefaultAsync(emp => emp.NombreEmpresa == empresa.NombreEmpresa);

            if (empresaExiste is not null)
            {
                ModelState.AddModelError("NombreEmpresa", "Esta empresa ya existe");
                return View(empresaExiste);
            }
            
            //Variable para verificar si existe un usuario con el correo que el usuario proveyó
            var usuarioExiste = await applicationDbContext.Users.FirstOrDefaultAsync(user => user.Email == empresa.EmailAdministrador);

            if (usuarioExiste is not null)
            {
                ModelState.AddModelError("EmailAdministrador", "Este correo ya está registrado");
                return View(empresaExiste);
            }
             
            //Instanciando el nuevo usuario con la clase IdentityUser
            var nuevoUsuario = new IdentityUser()
            {
                Email = empresa.EmailAdministrador,
                UserName = empresa.EmailAdministrador
            };

            //Usando propiedades sombra para asignarle el valor a las propiedades de la tabla de usuarios que no están presente
            //en la clase IdentityUser
            applicationDbContext.Entry(nuevoUsuario).Property("LastName").CurrentValue = empresa.ApellidoAdministrador;
            applicationDbContext.Entry(nuevoUsuario).Property("Name").CurrentValue = empresa.NombreAdministrador;

            //Instanciando la variable de la clase Empresas, una de las entidades del proyecto
            var nuevaEmpresa = new Empresas()
            {
                
                NombreEmpresa = empresa.NombreEmpresa,
                AdminEmpresaId = nuevoUsuario.Id,
                 
            };

            

            //Verificando si el usuario ha agregado una foto, y validando la extensión de esta
            if (empresa.LogoEmpresa is not null)
            {
                var fileExtension = Path.GetExtension(empresa.LogoEmpresa.FileName).ToLower();

                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                {
                    ModelState.AddModelError("LogoEmpresa", "Solo se permiten fotos");
                    return View(empresa);
                }

                var logo = await SaveLogo(empresa.LogoEmpresa);
                //Usando el servicio para almacenar fotos creado por mí  
                nuevaEmpresa.URL = logo.Name;
            }
            
            //Aclarando los cambios que haremos en el dbContext
            applicationDbContext.Add(nuevaEmpresa);

            //Guardando estos cambios
            await applicationDbContext.SaveChangesAsync();

            applicationDbContext.Entry(nuevoUsuario).Property("EmpresaId").CurrentValue = nuevaEmpresa.IdEmpresa;

            //Creando el usuario con el UserManager
            var result = await userManager.CreateAsync(nuevoUsuario, password: empresa.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(empresa);
        }

        private async Task<AlmacenadorArchivosResultado> SaveLogo(IFormFile logo)
        {
            var resultado = await almacenadorArchivos.AlmacenarLogoEmpresa(Logos, logo); 
            return resultado;

        }
    }
}