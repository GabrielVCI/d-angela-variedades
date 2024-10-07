using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Migrations;
using System.Reflection;

namespace d_angela_variedades.Interfaces
{
    public interface ISubCategoriaRepositorio
    {
        Task<Subcategoria> GuardarSubcategoria(SubcategoriaDTO subcategoria, int categoriaId);
        Task<bool> EditarSubcategoria(SubcategoriaDTO subcategoria, int subcategoriaId);
        Task<bool> EliminarSubcategoria(int subcategoriaId);
        Task<bool> Save();
        Task<bool> SubcategoriaPerteneceAlaCategoria(int categoriaId, int subcategoria);
        Task<bool> SubCategoriaExiste(string nombreSubcategoria);
        Task<Subcategoria> ObtenerSubcategoria(int subcategoriaId);
        Task<List<Subcategoria>> ObtenerSubcategoriasDeUnaCategoria(int categoriaId);    
    }
}
