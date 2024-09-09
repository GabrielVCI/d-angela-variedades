using d_angela_variedades.Helper;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace d_angela_variedades.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmpresasRepositorio empresasRepositorio;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public HomeController(ILogger<HomeController> logger, 
                              IEmpresasRepositorio empresasRepositorio, 
                              IServiciosUsuarios serviciosUsuarios)
        {
            _logger = logger;
            this.empresasRepositorio = empresasRepositorio;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var empresa = await empresasRepositorio.ObtenerEmpresa(usuarioId);
            
            var model = new EmpresasViewModel();

            model.NombreEmpresa = empresa.NombreEmpresa;
            model.LogoURL = empresa.URL;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
