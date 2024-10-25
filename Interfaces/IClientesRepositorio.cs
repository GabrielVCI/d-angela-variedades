using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using Microsoft.AspNetCore.Routing.Constraints;

namespace d_angela_variedades.Interfaces
{
    public interface IClientesRepositorio
    {
        Task<List<Cliente>> ObtenerListadoClientes(int empresaId);
        Task<Cliente> ObtenerClienteAEditar(Guid clienteId);
        Task<bool> GuardarCliente(ClienteDTO cliente, int empresaId);
        Task<bool> EliminarCliente(Guid clienteId);
        Task<bool> EditarCliente(ClienteDTO cliente, Guid clienteId);
        Task<List<Cliente>> FiltrarClientePorNombreOTelefono(string nombre_telefono);
        Task<bool> ClienteExiste(Guid clienteId);
        Task<bool> ClientePerteneceAlaEmpresa(int empresaId);
        Task<bool> ClienteConElMismoNombre(string nombreCliente, int empresaId);
        Task<bool> ClienteConElMismoTelefono(long telefonoCliente, int empresaId);
        Task<bool> EditarClienteCambioDeNombre(string nombreCliente, Guid clienteId, int empresaId);
        Task<bool> EditarClienteCambioDeTelefono(long telefonoCliente, Guid clienteId, int empresaId); 
        Task<bool> Save();
    }
}
