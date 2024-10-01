using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.Controllers
{

    public class ProductosController : Controller
    {
        public ProductosController() { }

        public IActionResult Productos()
        {
            return View();
        }

        public IActionResult ProductosPorCategoria()
        {
            return View();
        }

        public IActionResult ProductosPorSubcategoria()
        {
            return View();
        }
    }
}
