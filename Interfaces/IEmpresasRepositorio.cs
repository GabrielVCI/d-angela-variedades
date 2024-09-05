using d_angela_variedades.Models;

namespace d_angela_variedades.Interfaces
{
    public interface IEmpresasRepositorio
    {
        Task<bool> Guardar();

        Task<bool> AgregarEmpresa(EmpresasViewModel empresas, string AdminId);
    }
}
