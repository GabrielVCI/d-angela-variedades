using d_angela_variedades.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace d_angela_variedades.Servicios
{
    public class ServiciosUsuarios : IServiciosUsuarios
    {
        private HttpContext httpContext;

        public ServiciosUsuarios(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContext = httpContextAccessor.HttpContext;
        }
        public string ObtenerUsuarioId()
        {
            if (httpContext.User.Identity.IsAuthenticated) //Si esta autenticado
            {
                var idClaim = httpContext.User.Claims
                    .Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault(); //Buscamos claims (informacion del usuario)
                                                                                       //y vemos si tiene el id en esa informacion

                return idClaim.Value;
            }

            else
            {
                throw new Exception("El usuario no esta autenticado");
            }
        }
    }
}
