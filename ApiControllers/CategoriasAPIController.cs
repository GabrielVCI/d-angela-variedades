using d_angela_variedades.Data;
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

            var empresaUsuarioId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var listadoCategorias = await categoriaRepositorio.ListadoDeCategorias(empresaUsuarioId);

            return listadoCategorias;
        }

        [HttpGet("{categoriaId:int}")]
        public async Task<ActionResult<Categoria>> Get(int categoriaId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaUsuarioId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            if(categoriaId is 0)
            {
                ModelState.AddModelError("", "Error al intentar editar la categoria");
                return StatusCode(400, ModelState);
            }

            var categoria = await categoriaRepositorio.ObtenerCategoria(categoriaId, empresaUsuarioId);

            if (categoria is null) 
            {
                return NotFound();
            }

            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post([FromBody] CategoriaEditarDTO nombreCategoria)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (nombreCategoria == null)
            {
                return BadRequest();
            }

            var empresaUsuarioId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var nuevaCategoria = await categoriaRepositorio.AgregarCategoria(nombreCategoria, empresaUsuarioId);

            if(nuevaCategoria is null)
            {
                return BadRequest();
            }

            return Ok(nuevaCategoria);
        }


        [HttpPut("{categoriaId:int}")]
        public async Task<ActionResult<Categoria>> Put([FromBody] CategoriaEditarDTO categoria, int categoriaId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();  

            var empresaUsuarioId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            if(empresaUsuarioId is 0)
            {
                ModelState.AddModelError("", "Esta categoria no pertenece a la empresa");
                return StatusCode(403, ModelState);
            }

            if(categoria is null)
            {
                ModelState.AddModelError("", "Error al editar la categoria");
                return StatusCode(400, ModelState);
            }

            var categoriaEditada = await categoriaRepositorio.EditarCategoria(categoria, categoriaId, empresaUsuarioId);

            if (!categoriaEditada)
            {
                ModelState.AddModelError("", "Error en el servidor");
                return StatusCode(500, ModelState);
            }

            return Ok(categoriaEditada);
        }

        [HttpDelete("{categoriaId:int}")]
        public async Task<ActionResult<Categoria>> Delete(int categoriaId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaUsuarioId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            if(empresaUsuarioId is 0)
            {
                return StatusCode(400, "Esta categoría no pertenece a esta empresa.");
            }

            var categoria = await categoriaRepositorio.EliminarCategoria(empresaUsuarioId, categoriaId);

            if (!categoria)
            {
                return StatusCode(500, "Error al eliminar la categoria");
            }

            return Ok();
        }
    }
}
