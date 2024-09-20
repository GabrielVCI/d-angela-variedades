using d_angela_variedades.Models;

namespace d_angela_variedades.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task ObtenerIdUsuario();
        Task<bool> GuardarNuevoUsuario(EmpresasViewModel empresa, string EmpresaId);
        Task<bool> Guardar();
        Task<string> ObtenerNombreUsuario(string usuarioId);
        Task<int> ObtenerEmpresaUsuario(string usuarioId);

    }
}
