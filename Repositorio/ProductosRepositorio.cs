using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Repositorio
{
    public class ProductosRepositorio : IProductosRepositorio
    {
        private readonly ApplicationDbContext context;
        private readonly IServicioProductos servicioProductos;

        public ProductosRepositorio(ApplicationDbContext context, IServicioProductos servicioProductos)
        {
            this.context = context;
            this.servicioProductos = servicioProductos;
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
            nuevoProducto.CodigoProducto = await servicioProductos.GenerarCodigoDeProductoUnico();

            context.Add(nuevoProducto);

            return await Save();
        }

        public async Task<bool> EditarProducto(ProductosDTO productosDTO, Guid productoId)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.IdProducto == productoId);

            if (producto is null)
            {
                return false;
            }

            producto.Nombre = productosDTO.Nombre;
            producto.Descripcion = productosDTO.Descripcion;
            producto.Stock = productosDTO.Stock;
            producto.Precio = productosDTO.Precio;
            producto.IdCategoria = productosDTO.IdCategoria;
            producto.IdSubCategoria = productosDTO.IdSubCategoria;

            context.Update(producto);

            return await Save();

        }

        public async Task<List<Productos>> ObtenerListadoProductos(int empresaId)
        {
            var productos = await context.Productos.Where(p => p.EmpresaId == empresaId).ToListAsync();

            return productos;
        }

        public async Task<Productos> ObtenerProducto(Guid productoId, int empresaId)
        {
            var producto = await context.Productos
                .FirstOrDefaultAsync(prod => prod.IdProducto == productoId && prod.EmpresaId == empresaId);

            return producto;
        }

        public async Task<bool> ProductoExiste(Guid productoId)
        {
            var productoExiste = await context.Productos.AnyAsync(prod => prod.IdProducto == productoId);

            return productoExiste;
        }

        public async Task<bool> ProductoPerteneceAlaEmpresa(Guid productoId, int empresaId)
        {
            var productoPerteneceAlaEmpresa = await context.Productos.AnyAsync(prod => prod.IdProducto == productoId && prod.EmpresaId == empresaId);

            return productoPerteneceAlaEmpresa;
        }

        public async Task<bool> Save()
        {
            var save = await context.SaveChangesAsync();

            return save > 0 ? true : false;
        }
    }
}
