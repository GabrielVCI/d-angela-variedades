using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;

namespace d_angela_variedades.Repositorio
{
    public class CategoriaRepositorio : ICategoriasRepositorio
    {
        private readonly ApplicationDbContext context;
        private readonly IUsuariosRepositorio usuariosRepositorio;

        public CategoriaRepositorio(ApplicationDbContext context, IUsuariosRepositorio usuariosRepositorio) {
            this.context = context;
            this.usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<Categoria> AgregarCategoria(Categoria nombreCategoria, int empresaUsuarioId)
        { 
            var nuevaCategoria = new Categoria();

            nuevaCategoria.Nombre = nombreCategoria.Nombre;
            nuevaCategoria.EmpresaId = empresaUsuarioId;
            
            context.Add(nuevaCategoria);

            await Save();

            return nuevaCategoria;
        }

        public async Task<List<Categoria>> ListadoDeCategorias(int empresaUsuarioId)
        {

            var categorias = await context.Categorias.Where(cat => cat.EmpresaId == empresaUsuarioId).ToListAsync();
            return categorias;
        }

        public async Task<bool> Save()
        {
            var save = await context.SaveChangesAsync();

            return save > 0 ? true : false; 
        }
    }
}
