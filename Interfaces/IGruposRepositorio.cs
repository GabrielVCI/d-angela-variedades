using d_angela_variedades.Data;
using d_angela_variedades.Entidades;

namespace d_angela_variedades.Interfaces
{
    public interface IGruposRepositorio
    {
        Task<List<Grupos>> ObtenerGrupos(int empresaId);
        Task<Grupos> ObtenerGrupoAEditar(int grupoId);
        Task<bool> GrupoExiste(int grupoId);
        Task<bool> GuardarGrupo(GrupoDTO grupoDTO, int empresaId);
        Task<bool> EditarGrupo(GrupoDTO grupoDTO, int grupoId);
        Task<bool> EliminarGrupo(int grupoId);
        Task<bool> Save();
    }
}
