using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace d_angela_variedades.Controllers
{
    public class CategoriasController : Controller
    {
        public CategoriasController()
        {

        }
        public ActionResult Index() 
        {
            return View();
        }
    }
}
