using d_angela_variedades.Entidades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UsuariosRepositorio(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }
        public async Task<bool> Guardar()
        {
            var save = await applicationDbContext.SaveChangesAsync(); 
            return save > 0? true: false; 
        }

        public async Task<bool> GuardarNuevoUsuario(EmpresasViewModel empresa, string EmpresaId)
        {
            var nuevoUsuario = new IdentityUser()
            {
                UserName = empresa.EmailAdministrador,
                Email = empresa.EmailAdministrador
            };

            applicationDbContext.Entry(nuevoUsuario).Property("LastName").CurrentValue = empresa.ApellidoAdministrador;
            applicationDbContext.Entry(nuevoUsuario).Property("Name").CurrentValue = empresa.NombreAdministrador;
            applicationDbContext.Entry(nuevoUsuario).Property("EmpresaId").CurrentValue = EmpresaId;

            var resultado = await userManager.CreateAsync(nuevoUsuario, password: empresa.Password);

            return resultado.Succeeded ? true: false;  
        }

        public Task ObtenerIdUsuario()
        {
            throw new NotImplementedException();
        }

        public async Task<string> ObtenerNombreUsuario(string usuarioId)
        {
            var usuario = await applicationDbContext.Users
                .Where(u => u.Id == usuarioId)
                .Select(u => new
                {
                    u.UserName,
                    Name = EF.Property<string>(u, "Name"),
                    
                })
                .FirstOrDefaultAsync();

            if(usuario is null)
            {
                return "";
            }

            return usuario.Name;
        }

        public async Task<int> ObtenerEmpresaUsuario(string usuarioId)
        {
            var usuario = await applicationDbContext.Users.Where(user => user.Id == usuarioId).Select(user => new
            {
                user.Id,
                EmpresaId = EF.Property<int>(user, "EmpresaId")
            }).FirstOrDefaultAsync();

            if(usuario is null)
            {
                return -1;
            }

            return usuario.EmpresaId;
        }
    }
}
