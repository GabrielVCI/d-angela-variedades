using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Repositorio
{
    public class SubCategoriaRepositorio : ISubCategoriaRepositorio
    {
        private readonly ApplicationDbContext context;

        public SubCategoriaRepositorio(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Task<bool> EditarSubcategoria(Subcategoria subcategoria, int subcategoriaId, int categoriaId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarSubcategoria(int subcategoriaId, int categoriaId)
        {
            throw new NotImplementedException();
        }

        public async Task<Subcategoria> GuardarSubcategoria(SubcategoriaDTO subcategoria, int categoriaId)
        {
            var nuevaSubcategoria = new Subcategoria();

            nuevaSubcategoria.Name = subcategoria.Name;
            nuevaSubcategoria.IdCategoria = categoriaId;

            context.Add(nuevaSubcategoria);

            await Save();

            return nuevaSubcategoria;
            
        }

        public async Task<bool> Save()
        {
            var save = await context.SaveChangesAsync();

            return save > 0 ? true : false;
        }

        public async Task<bool> SubCategoriaExiste(string NombreSubcategoria)
        {
            var subcategoriaExiste = await context.Subategorias.AnyAsync(subcat => subcat.Name == NombreSubcategoria);  

            return subcategoriaExiste;
        }
    }
}
