using d_angela_variedades.Data;
using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Migrations;
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

        public async Task<bool> ClienteConElMismoNombre(string nombreCliente, int empresaId)
        {
            var cliente = await context.Clientes.AnyAsync(cli => cli.Nombre == nombreCliente && cli.EmpresaId == empresaId); 
            return cliente;
        }

        public async Task<bool> ClienteConElMismoTelefono(long telefonoCliente, int empresaId)
        {
            var cliente = await context.Clientes.AnyAsync(cli => cli.Telefono == telefonoCliente && cli.EmpresaId == empresaId);
            return cliente;
        }

        public async Task<bool> ClienteExiste(Guid clienteId)
        {
            var clienteExiste = await context.Clientes.AnyAsync(cli => cli.IdCliente == clienteId);

            return clienteExiste;
        }

        public async Task<bool> ClientePerteneceAlaEmpresa(int empresaId)
        {
            var clientePerteneceAlaEmpresa = await context.Clientes.AnyAsync(cli => cli.EmpresaId == empresaId);

            return clientePerteneceAlaEmpresa;
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

        //Este metodo es para asegurarme de que el usuario ingrese un nombre diferente a los que ya estan
        //registrados, excepto si es el del cliente que esta editando.
        public async Task<bool> EditarClienteCambioDeNombre(string nombreCliente, Guid clienteId, int empresaId)
        {
            var cliente = await context.Clientes
                .AnyAsync(cli => cli.Nombre == nombreCliente && cli.IdCliente != clienteId && cli.EmpresaId == empresaId);

            return cliente;
        }

        public async Task<bool> EditarClienteCambioDeTelefono(long telefonoCliente, Guid clienteId, int empresaId)
        {
            var cliente = await context.Clientes
                .AnyAsync(cli => cli.IdCliente != clienteId && cli.Telefono == telefonoCliente && cli.EmpresaId == empresaId);

            return cliente;
        }

        public async Task<bool> EliminarCliente(Guid clienteId)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(cli => cli.IdCliente == clienteId);

            context.Remove(cliente);

            return await Save();
        }

        public async Task<List<Cliente>> FiltrarClientePorNombreOTelefono(string nombre_telefono)
        {

            var clientes = new List<Cliente>();
            long telefono;

            // Si la cadena ingresada es un número, busca por teléfono
            if (long.TryParse(nombre_telefono, out telefono))
            {
                clientes = await context.Clientes
                    .Where(cli => cli.Telefono == telefono || cli.Nombre.Contains(nombre_telefono))
                    .ToListAsync();
            }
            else
            {
                // Si la cadena no es un número, busca solo por nombre
                clientes = await context.Clientes
                    .Where(cli => cli.Nombre.Contains(nombre_telefono))
                    .ToListAsync();
            }

            return clientes;

        }

        public async Task<bool> GuardarCliente(ClienteDTO cliente, int empresaId)
        {
            var nuevoCliente = new Cliente()
            {
                Nombre = cliente.NombreCliente,
                Telefono = cliente.Telefono,
                Nota = cliente.Nota,
                GrupoId = cliente.GrupoId,
                EmpresaId = empresaId
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
