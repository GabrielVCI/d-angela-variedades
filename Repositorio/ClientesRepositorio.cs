using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Repositorio
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly ApplicationDbContext context;

        public ClientesRepositorio(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> ClienteExiste(Guid clienteId)
        {
            var clienteExiste = await context.Clientes.AnyAsync(cli => cli.IdCliente == clienteId);

            return clienteExiste;
        }

        public async Task<bool> EditarCliente(ClienteDTO cliente, Guid clienteId)
        {
            var clienteAEditar = await context.Clientes.FirstOrDefaultAsync(cli => cli.IdCliente == clienteId);

            clienteAEditar.Nombre = cliente.NombreCliente;
            clienteAEditar.Nota = cliente.Nota;
            clienteAEditar.Telefono = cliente.Telefono;
            clienteAEditar.GrupoId = cliente.GrupoId;

            context.Update(clienteAEditar);

            return await Save();
        }

        public async Task<bool> EliminarCliente(Guid clienteId)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(cli => cli.IdCliente == clienteId);

            context.Remove(cliente);

            return await Save();
        }

        public async Task<List<Cliente>> FiltrarClientePorNombreOTelefono(string nombre_telefono)
        {
            var clientes = await context.Clientes
                .Where(cli => cli.Nombre == nombre_telefono || cli.Telefono == Convert.ToInt32(nombre_telefono)).ToListAsync();

            return clientes;
        }

        public async Task<bool> GuardarCliente(ClienteDTO cliente)
        {
            var nuevoCliente = new Cliente()
            {
                Nombre = cliente.NombreCliente,
                Telefono = cliente.Telefono,
                Nota = cliente.Nota,
                GrupoId = cliente.GrupoId
            };

            context.Add(nuevoCliente);

            return await Save();
        }

        public async Task<Cliente> ObtenerClienteAEditar(Guid clienteId)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(cli => cli.IdCliente == clienteId);

            return cliente;
        }

        public async Task<List<Cliente>> ObtenerListadoClientes(int empresaId)
        {
            var listadoClientes = await context.Clientes.ToListAsync();

            return listadoClientes;
        }

        public async Task<bool> Save()
        {
            var saved = await context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }
    }
}
