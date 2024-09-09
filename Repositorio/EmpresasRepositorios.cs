using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<string> ObtenerLogoURLEmpresa(int empresaId)
        {
            var empresa = await applicationDbContext.Empresas.FirstOrDefaultAsync(emp => emp.IdEmpresa == empresaId);

            return empresa.URL;
        }

        public async Task<Empresas> ObtenerEmpresa(string usuarioId)
        {
            var usuario = await applicationDbContext.Users
                .Where(user => user.Id == usuarioId). 
                Select(u => new
                {
                    EmpresaId = EF.Property<int>(u, "EmpresaId"),
                }).FirstOrDefaultAsync();

            var empresa = await applicationDbContext.Empresas.FirstOrDefaultAsync(emp => emp.IdEmpresa == usuario.EmpresaId);

            if(empresa is null)
            {
                return null;
            }

            return empresa;
        }

        
    }
}
