using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Models;
using Microsoft.AspNetCore.Identity;

namespace d_angela_variedades.Repositorio
{
    public class EmpresasRepositorios : IEmpresasRepositorio
    {
        private readonly ApplicationDbContext applicationDbContext;
 
        public EmpresasRepositorios(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
         }
        public async Task<bool> AgregarEmpresa(EmpresasViewModel empresas, string AdminId)
        {
            var nuevaEmpresa = new Empresas()
            {
                AdminEmpresaId = AdminId,
                NombreEmpresa = empresas.NombreEmpresa,
            };

            applicationDbContext.Add(nuevaEmpresa);
            return await Guardar();

        }

        public async Task<bool> Guardar()
        {
            var save = await applicationDbContext.SaveChangesAsync();

            return save > 0? true : false;
        }
    }
}
