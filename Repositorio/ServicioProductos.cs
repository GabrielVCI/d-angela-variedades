using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Repositorio
{
    public class ServicioProductos : IServicioProductos
    {
        private readonly ApplicationDbContext context;
        private static Random random = new Random();
        public ServicioProductos(ApplicationDbContext context)
        {
            this.context = context;
        }
         
        public async Task<int> GenerarCodigoDeProductoUnico()
        {
            int codigo;

            do
            {
                codigo = GenerateRandomCode();

            } while (await context.Productos.AnyAsync(prod => prod.CodigoProducto == codigo));

            return codigo;
        }

        private int GenerateRandomCode()
        {
            // Thread-safe random generation for a 5-digit code
            lock (random)
            {
                return random.Next(10000, 99999);
            }
        }
    }
}
