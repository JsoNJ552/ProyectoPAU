using Microsoft.EntityFrameworkCore;
using ProyectoPAU;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProyectoPAU.Models;
using ProyectoPAU.Services.Auth;
using ProyectoPAU.Services.Registro;
using ProyectoPAU.Services.ProductoService.ProductoService;
using ProyectoPAU.Services.ProductoService;
using ProyectoPAU.Services.CategoriasService;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProyectoPAU.Services.LoginService;
using ProyectoPAU.Services.CarService;
using ProyectoPAU.Services.VentasService;
using ProyectoPAU.Services.UsuariosService;
using ProyectoPAU.Services.ProductosInicioService;
using ProyectoPAU.Controllers;
internal class Program
{
    private static void Main(string[] args)
    {
        

        // ...

        var builder = WebApplication.CreateBuilder(args);

        // Agregar servicios al contenedor.
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IRegistroUsuario, RegistroService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();
        builder.Services.AddScoped<ICategoriasService, CategoriasService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IProductosInicioService, ProductosInicioService>();

        builder.Services.AddScoped<ICarService, CarService>();
       
        builder.Services.AddScoped< CarritoDetalle>();
        builder.Services.AddScoped<Carrito>();
        builder.Services.AddScoped<CarritoActionFilter>();
        builder.Services.AddScoped<IVentasServicecs, VentaService>();
        builder.Services.AddScoped<IUsuariosService, UsuariosService>();
        builder.Services.AddScoped< DetalleVenta>();
        builder.Services.AddHttpContextAccessor();

        // Configurar la base de datos
        builder.Services.AddDbContext<TiendauContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

       

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "MyCookieAuthentication";
                options.LoginPath = "/Usuario/Login";
                options.LogoutPath = "/Usuario/CerrarSesion";
                options.AccessDeniedPath = "/Home/Index";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

        builder.Services.AddSession(options =>
        {
            options.Cookie.Name = "MySessionCookie"; // Nombre de la cookie de sesi�n
            options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de espera de inactividad de la sesi�n
            options.Cookie.HttpOnly = true; // Solo accesible a trav�s de HTTP
            options.Cookie.IsEssential = true; // Marca la cookie como esencial
        });


        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(typeof(CarritoActionFilter));
        });

                    builder.Services.AddEndpointsApiExplorer();

                    builder.Services.AddSwaggerGen();





        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

                    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};




        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Agregar el middleware de sesi�n
        app.UseSession();

        app.UseAuthentication();

        app.UseAuthorization();
      

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

                   

        app.Run();
    }
}