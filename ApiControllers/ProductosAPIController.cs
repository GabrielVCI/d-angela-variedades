using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace d_angela_variedades.ApiControllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosAPIController : ControllerBase
    {
        private readonly ICategoriasRepositorio categoriaRepositorio;
        private readonly IProductosRepositorio productosRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public ProductosAPIController(ICategoriasRepositorio categoriaRepositorio, IProductosRepositorio productosRepositorio, IUsuariosRepositorio usuariosRepositorio, IServiciosUsuarios serviciosUsuarios)
        {
            this.categoriaRepositorio = categoriaRepositorio;
            this.productosRepositorio = productosRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        [HttpGet]
        public async Task<List<Productos>> Get()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var productos = await productosRepositorio.ObtenerListadoProductos(empresaId);

            return productos;
        }

        [HttpGet("{productoId:Guid}")]
        public async Task<Productos> Get(Guid productoId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var producto = await productosRepositorio.ObtenerProducto(productoId, empresaId);

            return producto;
        }

        [HttpGet("obtenerProductosPorElNombre")]
        public async Task<ActionResult<List<Productos>>> Get([FromQuery] string? nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return StatusCode(400, "Error");
            }
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var productoExiste = await productosRepositorio.ProductoExistePorNombre(nombre);

            if (!productoExiste)
            {
                return StatusCode(404, "No existen productos con este nombre");
            }

            var productosPertenecenAlaEmpresa = await productosRepositorio.ProductoPerteneceAlaEmpresaPorNombre(nombre, empresaId);

            if (!productosPertenecenAlaEmpresa)
            {
                return StatusCode(400, "No se pueden obtener los productos");
            }

            var productosPorNombre = await productosRepositorio.ListadoProductosPorNombre(nombre);

            return productosPorNombre;
        }

        [HttpGet("filtrarProductos")]
        public async Task<ActionResult<List<Productos>>> GetProductosFiltrados([FromQuery] string? nombreProducto, int? precio, int? stock, int? categoriaId, int? subcategoriaId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var productos = await productosRepositorio.ListadoProductosFiltrados(nombreProducto,  precio, stock, categoriaId,  subcategoriaId, empresaId);

            if (productos is null)
            {
                StatusCode(500, "Error al obtener los productos");
            }

            if(productos.Count() == 0)
            {
                return StatusCode(404, "No existe un producto con esas caracteristicas.");
            }

            return productos;

        }

        [HttpPost]
        public async Task<ActionResult<Productos>> Post([FromBody] ProductosDTO productosDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(500, "Error en el servidor");
            }

            if(productosDTO is null)
            {
                return StatusCode(400, "Error con el producto");
            }

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            //Variable para verificar si la categoria existe
            var categoriaExiste = await categoriaRepositorio.CategoriaExiste(productosDTO.IdCategoria);

            var categoriaPerteneceAlaEmpresa = await categoriaRepositorio.CategoriaPerteneceAlaEmpresa(empresaId, productosDTO.IdCategoria);

            if (!categoriaExiste || !categoriaPerteneceAlaEmpresa)
            {
                return StatusCode(400, "Error");
            }

            var producto = await productosRepositorio.AgregarProducto(productosDTO, empresaId);

            if (!producto)
            {
                return StatusCode(500, "Error al agregar el producto");
            }

            return Ok();

        }

        [HttpPut("{productoId:Guid}")]
        public async Task<ActionResult<Subcategoria>> Put([FromBody] ProductosDTO productosDTO, Guid productoId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var productoExiste = await productosRepositorio.ProductoExiste(productoId);

            if(!productoExiste)
            {
                return StatusCode(404, "El producto no existe");
            }

            var productoPerteneceAlaEmpresa = await productosRepositorio.ProductoPerteneceAlaEmpresa(productoId, empresaId);

            if (!productoPerteneceAlaEmpresa)
            {
                return StatusCode(403, "No puedes editar este producto porque no pertenece a tu empresa");
            }

            var producto = await productosRepositorio.EditarProducto(productosDTO, productoId);

            if (!producto)
            {
                return StatusCode(500, "Error al guardar el producto");
            }

            return Ok();
        }

        [HttpDelete("{productoId:Guid}")]
        public async Task<ActionResult> Delete(Guid productoId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var productoExiste = await productosRepositorio.ProductoExiste(productoId);
            if (!productoExiste) 
            {
                return StatusCode(404, "El producto no ha sido encontrado.");
            }

            var productoPertenceAlaEmpresa = await productosRepositorio.ProductoPerteneceAlaEmpresa(productoId, empresaId);
            if (!productoPertenceAlaEmpresa)
            {
                return StatusCode(400, "El producto no pertenece a la empresa");
            }

            var elimacionDelProducto = await productosRepositorio.EliminarProducto(productoId);

            if(!elimacionDelProducto)
            {
                return StatusCode(500, "Ha ocurrido un error al intentar eliminar el producto");
            }

            return Ok();
        }

    }
}
