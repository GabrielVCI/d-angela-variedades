using d_angela_variedades.Data;
using d_angela_variedades.Entidades;

namespace d_angela_variedades.Interfaces
{
    public interface IClientesRepositorio
    {
        Task<List<Cliente>> ObtenerListadoClientes(int empresaId);
        Task<Cliente> ObtenerClienteAEditar(Guid clienteId);
        Task<bool> GuardarCliente(ClienteDTO cliente);
        Task<bool> EliminarCliente(Guid clienteId);
        Task<bool> EditarCliente(ClienteDTO cliente, Guid clienteId);
        Task<List<Cliente>> FiltrarClientePorNombreOTelefono(string nombre_telefono);
        Task<bool> ClienteExiste(Guid clienteId);
        Task<bool> Save();
    }
}
