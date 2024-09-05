using d_angela_variedades;
using d_angela_variedades.Interfaces;
using d_angela_variedades.Repositorio;
using d_angela_variedades.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure connectionStrings
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

//Configure the Identity FC usage
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.Password.RequireNonAlphanumeric = false;
    opciones.Password.RequiredLength = 6;
    opciones.Password.RequiredUniqueChars = 0;
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IVentasRepositorio, VentasRepositorio>();
builder.Services.AddTransient<ICategoriasRepositorio, CategoriaRepositorio>();
builder.Services.AddTransient<ISubCategoriaRepositorio, SubCategoriaRepositorio>();
builder.Services.AddTransient<IProductosRepositorio, ProductosRepositorio>();
builder.Services.AddTransient<IEmpresasRepositorio, EmpresasRepositorios>();
builder.Services.AddTransient<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddTransient<IServiciosUsuarios, ServiciosUsuarios>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
