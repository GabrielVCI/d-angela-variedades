using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.Controllers
{
    public class ClientesController : Controller
    {
        public ClientesController() { }


        public ActionResult Index() { return View(); }

        public ActionResult About() { return View(); }
    }
}
