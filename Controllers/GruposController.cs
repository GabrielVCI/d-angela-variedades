using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.Controllers
{
    public class GruposController : Controller
    {
        public GruposController() { }

        public ActionResult Index()
        {
            return View();
        }
    }
}
