using d_angela_variedades.Data;
using d_angela_variedades.Entidades;

namespace d_angela_variedades.Interfaces
{
    public interface ICategoriasRepositorio
    {
        Task<List<Categoria>> ListadoDeCategorias(int empresaUsuarioId);
        Task<Categoria> AgregarCategoria(Categoria nombreCategoria, int empresaUsuarioId);
        Task<bool> Save();
        Task<Categoria> ObtenerCategoria(int categoriaId, int empresaCategoriaId);
        Task<bool> EditarCategoria(CategoriaEditarDTO categoria, int categoriaId, int empresaUsuarioId);
        Task<bool> EliminarCategoria(int empresaUsuarioId, int categoriaId);
    }
}
