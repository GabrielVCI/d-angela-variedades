using d_angela_variedades.Models;

namespace d_angela_variedades.Interfaces
{
    public interface IAlmacenadorArchivos
    {
        Task Borrar(string ruta, string contenedor);
        Task<AlmacenadorArchivosResultado> AlmacenarLogoEmpresa(string contenedor, IFormFile logo);
    }
}
