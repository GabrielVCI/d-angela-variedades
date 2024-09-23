using d_angela_variedades.Entidades;

namespace d_angela_variedades.Interfaces
{
    public interface ICategoriasRepositorio
    {
        Task<List<Categoria>> ListadoDeCategorias(int empresaUsuarioId);
        Task<Categoria> AgregarCategoria(Categoria nombreCategoria, int empresaUsuarioId);
        Task<bool> Save();
    }
}
