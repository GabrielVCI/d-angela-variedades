using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.API
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasAPIController : ControllerBase
    {
        private readonly ICategoriasRepositorio categoriaRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public CategoriasAPIController(ICategoriasRepositorio categoriaRepositorio, IUsuariosRepositorio usuariosRepositorio, IServiciosUsuarios serviciosUsuarios)
        {
            this.categoriaRepositorio = categoriaRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
            this.serviciosUsuarios = serviciosUsuarios;
        }


        [HttpGet]
        public async Task<List<Categoria>> Get()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaUsuarioId = await usuariosRepositorio.ObtenerEmpresaUsuario(usuarioId);

            var listadoCategorias = await categoriaRepositorio.ListadoDeCategorias(empresaUsuarioId);

            return listadoCategorias;
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post([FromBody] Categoria nombreCategoria)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            if (!ModelState.IsValid)
            {
                Console.WriteLine("aja");
                return BadRequest(ModelState);
            }

            if (nombreCategoria == null)
            {
                return BadRequest();
            }

            var empresaUsuarioId = await usuariosRepositorio.ObtenerEmpresaUsuario(usuarioId);

            var nuevaCategoria = await categoriaRepositorio.AgregarCategoria(nombreCategoria, empresaUsuarioId);

            if(nuevaCategoria is null)
            {
                return BadRequest();
            }

            return Ok(nuevaCategoria);
        }
    }
}
