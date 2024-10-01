using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
