using d_angela_variedades.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property<string?>("LastName");
                entity.Property<string?>("Name");
                entity.Property<string?>("FTURL");
                entity.Property<int>("EmpresaId");
            });

            builder.Entity<Categoria>()
                    .HasMany(c => c.Subcategorias)
                    .WithOne(s => s.Categoria)
                    .HasForeignKey(s => s.IdCategoria);

            builder.Entity<Productos>()
                    .HasIndex(p => p.CodigoProducto)
                    .IsUnique();

         }
    }

    
}
