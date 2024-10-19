
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




        [HttpPost]  
        public async Task<ActionResult<GrupoDTO>> Post([FromBody] GrupoDTO grupoDTO)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            var grupo = await gruposRepositorio.GuardarGrupo(grupoDTO, empresaId);

            if (!grupo)
            {
                return StatusCode(500);
            }

            return Ok(grupo);
        }



    }
}
