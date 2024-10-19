using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace d_angela_variedades.ApiControllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesAPIController : ControllerBase
    {
        private readonly IServiciosUsuarios serviciosUsuarios;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IClientesRepositorio clientesRepositorio;

        public ClientesAPIController(IServiciosUsuarios serviciosUsuarios, IUsuariosRepositorio usuariosRepositorio, IClientesRepositorio clientesRepositorio)
        {
            this.serviciosUsuarios = serviciosUsuarios;
            this.usuariosRepositorio = usuariosRepositorio;
            this.clientesRepositorio = clientesRepositorio;
        }


        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var empresaId = await usuariosRepositorio.ObtenerEmpresaUsuarioId(usuarioId);

            if(empresaId <= 0)
            {
                return StatusCode(403);
            }

            var clientes = await clientesRepositorio.ObtenerListadoClientes(empresaId);

            return clientes;
        }

        [HttpGet("{clienteId:Guid}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid clienteId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var cliente = await clientesRepositorio.ClienteExiste(clienteId);

            if (!cliente)
            {
                return StatusCode(404, "Cliente no ha sido encontrado");
            }

            return Ok(cliente);
        }

        [HttpGet("obtenerClientesPorElNombreOTelefono")]
        public async Task<ActionResult<List<Cliente>>> ObtenerClienteFiltro([FromQuery] string nombre_telefono)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var clientes = await clientesRepositorio.FiltrarClientePorNombreOTelefono(nombre_telefono);

            if(clientes.Count() == 0)
            {
                return StatusCode(404);
            }

            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post([FromBody] ClienteDTO clienteDTO)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var cliente = await clientesRepositorio.GuardarCliente(clienteDTO);

            if (!cliente)
            {
                return StatusCode(500, "Error al guardar al cliente");
            }

            return Ok(cliente);
        }


        [HttpPut("{clienteId:Guid}")]
        public async Task<ActionResult<Cliente>> Put(ClienteDTO clienteDTO, Guid clienteId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var clienteExiste = await clientesRepositorio.ClienteExiste(clienteId);
            if (!clienteExiste)
            {
                return StatusCode(404);
            }

            var cliente = await clientesRepositorio.EditarCliente(clienteDTO, clienteId);

            if (!cliente)
            {
                return StatusCode(500, "Error al intentar editar el cliente");
            }

            return Ok(cliente);
        }

        [HttpDelete("{clienteId:Guid}")]
        public async Task<ActionResult> Delete(Guid clienteId)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var clienteExiste = await clientesRepositorio.ClienteExiste(clienteId);
            if (!clienteExiste)
            {
                return StatusCode(404);
            }

            var cliente = await clientesRepositorio.EliminarCliente(clienteId);

            if (!cliente)
            {
                return StatusCode(500, "Error al guardar al cliente");
            }

            return Ok(cliente);
        }








    }
}
