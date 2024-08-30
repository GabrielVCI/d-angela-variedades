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

            builder.Entity<Categoria>()
                .HasKey(c => c.IdCategoria)
                .HasName("PrimaryKey_Categorias");            
            
            builder.Entity<Empresas>()
                .HasKey(e => e.IdEmpresa)
                .HasName("PrimaryKey_Empresas");            
            
            builder.Entity<Subcategoria>()
                .HasKey(s => s.IdSubCategoria)
                .HasName("PrimaryKey_SubCategorias");            
            
            builder.Entity<Ventas>()
                .HasKey(v => v.IdVenta)
                .HasName("PrimaryKey_Ventas");            
            
            builder.Entity<Productos>()
                .HasKey(p => p.IdProducto)
                .HasName("PrimaryKey_Producto");

        }
    }

    
}
