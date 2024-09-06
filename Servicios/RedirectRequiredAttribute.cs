using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.Servicios
{
    public class RedirectRequiredAttribute : ActionFilterAttribute
    {
        private readonly string _key;

        public RedirectRequiredAttribute(string key = "Redirected")
        {
            _key = key;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var controller = context.Controller as Controller;

            if (controller != null)
            {
                if (!controller.TempData.ContainsKey(_key))
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                }
                else
                {
                    controller.TempData.Remove(_key);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
