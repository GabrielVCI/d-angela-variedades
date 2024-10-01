using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.ApiControllers
{
    [Route("api/subcategorias")]
    [ApiController]
    public class SubcategoriasAPIController : ControllerBase
    {
        private readonly ICategoriasRepositorio categoriasRepositorio;
        private readonly IServiciosUsuarios servicioUsuario;
        private readonly ISubCategoriaRepositorio subCategoriaRepositorio;
        private readonly IUsuariosRepositorio usuariosRepositorio;

        public SubcategoriasAPIController(ICategoriasRepositorio categoriasRepositorio, IServiciosUsuarios servicioUsuario, ISubCategoriaRepositorio subCategoriaRepositorio, IUsuariosRepositorio usuariosRepositorio)
        {
            this.categoriasRepositorio = categoriasRepositorio;
            this.servicioUsuario = servicioUsuario;
            this.subCategoriaRepositorio = subCategoriaRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
        }
            
        [HttpPost("{categoriaId:int}")]
        public async Task<ActionResult<Subcategoria>> Post([FromBody] SubcategoriaDTO subcategoria, int categoriaId)
        {
            var usuarioId = servicioUsuario.ObtenerUsuarioId();

            if (subcategoria is null)
            {
                ModelState.AddModelError("", "Error al agregar subcategoria");
                return StatusCode(400, ModelState);
            }

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var categoriaExiste = await categoriasRepositorio.CategoriaExiste(categoriaId);

            if (!categoriaExiste)
            {
                ModelState.AddModelError("", "Categoria inexistente");
                return StatusCode(404, ModelState);
            }

            var categoriaPerteneceAlaEmpresa = await categoriasRepositorio.CategoriaPerteneceAlaEmpresa(empresaId, categoriaId);
            if (!categoriaPerteneceAlaEmpresa)
            {
                ModelState.AddModelError("", "Esta categoria no pertenece a su empresa");
                return StatusCode(500, ModelState);
            }


            var subcategoriaExiste = await subCategoriaRepositorio.SubCategoriaExiste(subcategoria.Name);
            if (subcategoriaExiste)
            {
                ModelState.AddModelError("", "Esta subcategoria ya existe");
                return StatusCode(422, ModelState);
            }

            var subcategoriaAGuardar = await subCategoriaRepositorio.GuardarSubcategoria(subcategoria, categoriaId); 

            if(subcategoriaAGuardar is null)
            {
                ModelState.AddModelError("", "Error al guardar la subcategoria");
                return StatusCode(500, ModelState);
            }

            return Ok(subcategoriaAGuardar);
        }

        [HttpGet("{subcategoriaId:int}")]
        public async Task<ActionResult<Subcategoria>> Get(int subcategoriaId)
        { 
            var subcategoria = await subCategoriaRepositorio.ObtenerSubcategoria(subcategoriaId);

            if( subcategoria is null)
            {
                ModelState.AddModelError("", "Error al obtener la subcategoria");
                return StatusCode(500, ModelState);
            }
            return subcategoria;
        }

        [HttpPut("{subcategoriaId:int}")]
        public async Task<ActionResult<Subcategoria>> Put([FromBody] SubcategoriaDTO subcategoria, int subcategoriaId)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(500, ModelState);
            }

            if(subcategoria is null)
            {
                return BadRequest();
            }

            var subcategoriaExiste = await subCategoriaRepositorio.SubCategoriaExiste(subcategoria.Name);
            if (subcategoriaExiste)
            {
                ModelState.AddModelError(string.Empty, "Esta subcategoria ya existe");
                return StatusCode(422, ModelState);
            }

            var subcategoriaAEditar = await subCategoriaRepositorio.EditarSubcategoria(subcategoria, subcategoriaId);

            if(!subcategoriaAEditar)
            {
                ModelState.AddModelError("", "Error al editar la subcategoria");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("{subcategoriaId:int}")]
        public async Task<ActionResult<Subcategoria>> Delete(int subcategoriaId)
        {
            var subcategoria = await subCategoriaRepositorio.EliminarSubcategoria(subcategoriaId);

            if(!subcategoria)
            {
                return StatusCode(500, "Error al eliminar la subcategoría");
            }

            return Ok(subcategoria);
        }
    }
}
