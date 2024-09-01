using d_angela_variedades.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace d_angela_variedades
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions dbContext) : base (dbContext)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }   
        public DbSet<Subcategoria> Subategorias { get; set; }   
        public DbSet<Ventas> Ventas { get; set; }   
        public DbSet<Productos> Productos{ get; set; }   
        public DbSet<Empresas> Empresas{ get; set; }   


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property<string?>("LastName");
                entity.Property<string?>("Name");
                entity.Property<string?>("FTURL");
            });
        }
    }

    
}
