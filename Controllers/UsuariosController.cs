using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Models;
using d_angela_variedades.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.Encodings.Web;

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
        private readonly IEmailSender emailSender;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string Logos = "LogoEmpresa";


        public UsuariosController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ApplicationDbContext applicationDbContext, IAlmacenadorArchivos almacenadorArchivos,
            IUsuariosRepositorio usuariosRepositorio, IEmpresasRepositorio empresasRepositorio, IServiciosUsuarios serviciosUsuarios, 
            IEmailSender _emailSender) 
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
            this.usuariosRepositorio = usuariosRepositorio;
            this.empresasRepositorio = empresasRepositorio;
            this.serviciosUsuarios = serviciosUsuarios;
            emailSender = _emailSender;
            this.almacenadorArchivos = almacenadorArchivos;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registro() 
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(EmpresasViewModel empresa)
        {

            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            //Variable para verificar si el nombre de la empresa ya fue registrado
            var empresaExiste = await applicationDbContext.Empresas.FirstOrDefaultAsync(emp => emp.NombreEmpresa == empresa.NombreEmpresa);

            if (empresaExiste is not null)
            {
                ModelState.AddModelError("NombreEmpresa", "Esta empresa ya existe");
                return View();
            }
            
            //Variable para verificar si existe un usuario con el correo que el usuario proveyó
            var usuarioExiste = await applicationDbContext.Users.FirstOrDefaultAsync(user => user.Email == empresa.EmailAdministrador);

            if (usuarioExiste is not null)
            {
                ModelState.AddModelError("EmailAdministrador", "Este correo ya está registrado");
                return View();
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
                    return View();
                }

                var logo = await SaveLogo(empresa.LogoEmpresa);
                //Usando el servicio para almacenar fotos creado por mí  
                nuevaEmpresa.URL = logo.Name;
            }

            nuevaEmpresa.URL = "";
            //Aclarando los cambios que haremos en el dbContext
            applicationDbContext.Add(nuevaEmpresa);

            //Guardando estos cambios
            await applicationDbContext.SaveChangesAsync();

            applicationDbContext.Entry(nuevoUsuario).Property("EmpresaId").CurrentValue = nuevaEmpresa.IdEmpresa;

            //Creando el usuario con el UserManager
            var result = await userManager.CreateAsync(nuevoUsuario, password: empresa.Password);

            if (result.Succeeded)
            {

                //We need to generate a email confirmation token in order to send an email to the new user
                var userToken = await userManager.GenerateEmailConfirmationTokenAsync(nuevoUsuario);

                //Creating an action to set the authentication process
                var callbackUrl = Url.Action(
                    "ConfirmEmail", //This is the action in the controller
                    "Usuarios", //this is the controller
                    new { userId = nuevoUsuario.Id, userToken = userToken }, //These are the parameters the action needs
                    protocol: HttpContext.Request.Scheme);


                //Call the email sender service to send the email
                await emailSender.SendEmailAsync(empresa.EmailAdministrador, "Confirma tu correo electronico",
                    $"Este mensaje es para verificar que posees un correo electrónico real. <br>" +
                    $"Una vez lo confirmes, serás redirigido al sitio de RealRDEstate. <br>" +
                    $"Por favor, confirma tu correo electrónico <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> aqui</a>");

                return RedirectToAction("_ConfirmEmail", "Usuarios");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (!ModelState.IsValid)
            {
                return View(loginView);
            }

            //Buscamos algun usuario que tenga como correo el otorgado por el usuario en el login.
            var usuario = await applicationDbContext.Users.FirstOrDefaultAsync(user => user.UserName == loginView.UserName);

            if (usuario is null)
            {
                ModelState.AddModelError("UserName", "Usuario no encontrado");
                return View(loginView);
            }

            var signInUser = await signInManager
                .PasswordSignInAsync(loginView.UserName, loginView.Password, loginView.RememberMe, lockoutOnFailure: false);

            if(signInUser.Succeeded && usuario.EmailConfirmed)
            {
                return RedirectToAction("Index", "Home");
            }

            else if (!usuario.EmailConfirmed)
            {
                //We need to generate a email confirmation token in order to send an email to the new user
                var userToken = await userManager.GenerateEmailConfirmationTokenAsync(usuario);

                //Creating an action to set the authentication process
                var callbackUrl = Url.Action(
                    "ConfirmEmail", //This is the action in the controller
                    "Usuarios", //this is the controller
                    new { userId = usuario.Id, userToken = userToken }, //These are the parameters the action needs
                    protocol: HttpContext.Request.Scheme);


                //Call the email sender service to send the email
                await emailSender.SendEmailAsync(loginView.UserName, "Confirma tu correo electronico",
                    $"Este mensaje es para verificar que posees un correo electrónico real. <br>" +
                    $"Una vez lo confirmes, serás redirigido al sitio de RealRDEstate. <br>" +
                    $"Por favor, confirma tu correo electrónico <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> aqui</a>");

                return RedirectToAction("_ConfirmEmail", "Usuarios");

            }

            else
            {
                ModelState.AddModelError("UserName", "Nombre de usuario o contraseña incorrecta");
                return View(loginView);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Password()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Password(ForgotPasswordViewMode model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            //Otherwise, we search the usercomplete info using the email the user provided
            var user = await userManager.FindByEmailAsync(model.Correo);

            //if the user is null (does not exist) or the email is not confirmed yet, return them to the login view
            if (user is null)
            {
                ModelState.AddModelError("ForgotPasswordUserEmail", "Correo no encontrado");
                return View(model);

            }

            if (!(await userManager.IsEmailConfirmedAsync(user)))
            {
                ModelState.AddModelError(string.Empty, "Correo no autenticado");
                return View(model);
            }

            //If not, generate a token to send it to the user email
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            //Create the redirect to the method we will use to actually reset the password
            var callbackUrl = Url.Action(
                "ResetPassword", "Usuarios",
                new { token, user.Email }, // Just pass the parameters the method we specified needs.
                protocol: HttpContext.Request.Scheme);

            //Then use the email sender method to send the email
            await emailSender.SendEmailAsync(
            model.Correo,
            "Recuperar Contraseña",
            $"Para cambiar tu contraseña, por favor entra a este <a href='{callbackUrl}'>enlace</a>");

            TempData["Redirected"] = true;
            return RedirectToAction("PasswordChangeValidation");

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword(string token = "", string email = "")
        {
            if (token is null || email is null){
                TempData["Redirected"] = true;
                return RedirectToAction("InvalidLink");
            }

            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            //If the model is not valid
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }

            //Otherwise, we find the user using the email they provided to us
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Email == model.Email);

            //If the user is null (does not exist)
            if (user is null)
            {
                
                return RedirectToAction("Login", "Usuarios");
            }

            //Otherwise, we reset the password, we do it by replace the old one with the new one
            //using the ResetPasswordAsync Method
            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            //If the result is succeeded, redirect the user to the confirmation reset  
            if (result.Succeeded)
            {
                TempData["Redirected"] = true;
                return RedirectToAction("ResetPasswordConfirmation");
            }

            //else, return the view
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult _ConfirmEmail()
        {
            return View();  
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string userToken)
        {
            //The users cannot be null
            if (userId == null || userToken == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Find the user by their Id
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Start the confirmation process
            var result = await userManager.ConfirmEmailAsync(user, userToken);

            //If everything good, register the user and redirect them to the home page
            if (result.Succeeded)
            {
                // Email confirmed successfully, you can redirect to a success page
                //If everything went good, sign in the user, add them to the "Comprador" Roles, and redirect them to home.
                //We use the signInManager,SignInAsync to sign in the user without a password
                await signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");

            }
            
            return View();
        }

        [RedirectRequired]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [RedirectRequired]
        [AllowAnonymous]
        public IActionResult PasswordChangeValidation()
        {
            return View();
        }

        [RedirectRequired]
        [AllowAnonymous]
        public IActionResult InvalidLink()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Usuarios");
        }

        private async Task<AlmacenadorArchivosResultado> SaveLogo(IFormFile logo)
        {
            var resultado = await almacenadorArchivos.AlmacenarLogoEmpresa(Logos, logo); 
            return resultado;

        }
    }
}