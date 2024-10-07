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
    }
}
