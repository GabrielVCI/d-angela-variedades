using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using System.Reflection;

namespace d_angela_variedades.Interfaces
{
    public interface ISubCategoriaRepositorio
    {
        Task<Subcategoria> GuardarSubcategoria(SubcategoriaDTO subcategoria, int categoriaId);
        Task<bool> EditarSubcategoria(Subcategoria subcategoria, int subcategoriaId, int categoriaId);
        Task<bool> EliminarSubcategoria(int subcategoriaId, int categoriaId);
        Task<bool> Save();
        Task<bool> SubCategoriaExiste(string NombreSubcategoria);
    }
}
