using d_angela_variedades.Data;
using d_angela_variedades.Entidades;

namespace d_angela_variedades.Interfaces
{
    public interface IProductosRepositorio
    {
        Task<List<Productos>> ObtenerListadoProductos(int empresaId);
        Task<bool> AgregarProducto(ProductosDTO producto, int empresaId);
        Task<bool> Save();
        Task<Productos> ObtenerProducto(Guid productoId, int empresaId);
        Task<bool> ProductoExiste(Guid productoId);
        Task<bool> ProductoPerteneceAlaEmpresa(Guid productoId, int empresaId);
        Task<bool> EditarProducto(ProductosDTO productosDTO, Guid productoId);
        Task<bool> EliminarProducto(Guid productoId);
        Task<bool> ProductoExistePorNombre(string nombreProducto);
        Task<bool> ProductoPerteneceAlaEmpresaPorNombre(string nombreProducto, int empresaId);
        Task<List<Productos>> ListadoProductosPorNombre(string nombreProducto);
        Task<List<Productos>> ListadoProductosFiltrados(string? nombreProducto, int? precio, int? stock, int? categoriaId, int? subcategoriaId, int empresaId);
    }
}
