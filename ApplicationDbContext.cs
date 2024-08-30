using d_angela_variedades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions dbContext) : base (dbContext)
        {
            
        }

        DbSet<Categoria> Categorias { get; set; }   
        DbSet<Subcategoria> Subategorias { get; set; }   
        DbSet<Ventas> Ventas { get; set; }   
        DbSet<Productos> Productos{ get; set; }   
        DbSet<Empresas> Empresas{ get; set; }   


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    
}
