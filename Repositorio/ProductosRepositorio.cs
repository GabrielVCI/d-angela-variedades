using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Repositorio
{
    public class ProductosRepositorio : IProductosRepositorio
    {
        private readonly ApplicationDbContext context;

        public ProductosRepositorio(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AgregarProducto(ProductosDTO producto, int empresaId)
        {
            var nuevoProducto = new Productos();

            nuevoProducto.Descripcion = producto.Descripcion;
            nuevoProducto.Stock = producto.Stock;
            nuevoProducto.Nombre = producto.Nombre;
            nuevoProducto.Precio = producto.Precio;
            nuevoProducto.IdSubCategoria = producto.IdSubCategoria;
            nuevoProducto.IdCategoria = producto.IdCategoria;
            nuevoProducto.EmpresaId = empresaId;

            context.Add(nuevoProducto);

            return await Save();
        }

        public async Task<List<Productos>> ObtenerListadoProductos(int empresaId)
        {
            var productos = await context.Productos.Where(p => p.EmpresaId == empresaId).ToListAsync();

            return productos;
        }

        public async Task<bool> Save()
        {
            var save = await context.SaveChangesAsync();

            return save > 0 ? true : false;
        }
    }
}
