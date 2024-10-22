
using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.ApiControllers
{
    [Route("api/grupos")]
    [ApiController]
    public class GruposAPIController : ControllerBase
    {
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IGruposRepositorio gruposRepositorio;

        public GruposAPIController(IServiciosUsuarios serviciosUsuarios, IUsuariosRepositorio usuariosRepositorio, IGruposRepositorio gruposRepositorio)
        {
            this.serviciosUsuarios = serviciosUsuarios;
            this.usuariosRepositorio = usuariosRepositorio;
            this.gruposRepositorio = gruposRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Grupos>>> Get()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var listadoDeGrupos = await gruposRepositorio.ObtenerGrupos(empresaId);

            if(listadoDeGrupos is null)
            {
                return StatusCode(400);
            }

            return Ok(listadoDeGrupos);
        }

        [HttpGet("{idGrupo:int}")]
        public async Task<ActionResult<GrupoDTO>> Get(int idGrupo)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var grupoExiste = await gruposRepositorio.GrupoExiste(idGrupo);

            if (!grupoExiste)
            {
                return StatusCode(404);
            }

            var grupoPerteneceAlaEmpresa = await gruposRepositorio.GrupoPerteneceAlaEmpresa(idGrupo, empresaId);

            if (!grupoPerteneceAlaEmpresa)
            {
                return StatusCode(403);
            }

            var grupo = await gruposRepositorio.ObtenerGrupoAEditar(idGrupo, empresaId);

            return Ok(grupo);
        }

        [HttpGet("obtenerGrupoConElNombre")]
        public async Task<ActionResult<List<Grupos>>> Get([FromQuery] string? nombreGrupo)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var grupos = await gruposRepositorio.ObtenerGrupoPorElNombre(nombreGrupo, empresaId);

            if(grupos is null)
            {
                return StatusCode(404);
            }

            return grupos;
        }

        [HttpPost]  
        public async Task<ActionResult<GrupoDTO>> Post([FromBody] GrupoDTO grupoDTO)
        {

            if(grupoDTO is null)
            {
                return StatusCode(500);
            }

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var grupo = await gruposRepositorio.GuardarGrupo(grupoDTO, empresaId);

            if (!grupo)
            {
                return StatusCode(500);
            }

            return Ok(grupo);
        }

        [HttpPut("{GrupoId:int}")]
        public async Task<ActionResult> Put([FromBody] GrupoDTO grupoDTO, int GrupoId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            if (usuarioId is null)
            {
                return StatusCode(403);
            }
            if (grupoDTO is null)
            {
                return StatusCode(500);
            }


            var grupoExiste = await gruposRepositorio.GrupoExiste(GrupoId);

            if (!grupoExiste)
            {
                return StatusCode(404);
            }

            var grupo = await gruposRepositorio.EditarGrupo(grupoDTO, GrupoId);

            if (!grupo)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete("{GrupoId:int}")]
        public async Task<ActionResult> Delete(int GrupoId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var grupo = await gruposRepositorio.EliminarGrupo(GrupoId, empresaId);

            if (!grupo)
            {
                return StatusCode(500);
            }

            return Ok(grupo);
        }

    }
}
