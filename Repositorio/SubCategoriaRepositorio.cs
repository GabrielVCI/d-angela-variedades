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
        public async Task<bool> EditarSubcategoria(SubcategoriaDTO subcategoria, int subcategoriaId)
        {
            var subcategoriaAEditar = await context.Subategorias.FirstOrDefaultAsync(subcat => subcat.IdSubCategoria == subcategoriaId);

            subcategoriaAEditar.Name = subcategoria.Name;

            context.Update(subcategoriaAEditar);
            return await Save();
        }

        public async Task<bool> EliminarSubcategoria(int subcategoriaId)
        {
            var subcategoria = await context.Subategorias.FirstOrDefaultAsync(subcat => subcat.IdSubCategoria == subcategoriaId);

            context.Remove(subcategoria);

            return await Save();
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

        public async Task<Subcategoria> ObtenerSubcategoria(int subcategoriaId)
        {
            var subcategoria = await context.Subategorias.FirstOrDefaultAsync(subcat => subcat.IdSubCategoria == subcategoriaId);

            return subcategoria;
        }

        public async Task<bool> Save()
        {
            var save = await context.SaveChangesAsync();

            return save > 0 ? true : false;
        }

        public async Task<bool> SubCategoriaExiste(string nombreSubcategoria)
        {
            var subcategoriaExiste = await context.Subategorias.AnyAsync(subcat => subcat.Name == nombreSubcategoria);  
            return subcategoriaExiste;
        }

        public async Task<bool> SubcategoriaPerteneceAlaCategoria(int categoriaId, int subcategoriaId)
        {
            var subcategoriaPerteneceAlacategoria = await 
                context.Subategorias
                .AnyAsync(subcat => 
                            subcat.IdCategoria == categoriaId && 
                            subcat.IdSubCategoria == subcategoriaId);

            return subcategoriaPerteneceAlacategoria;

        }
    }
}
