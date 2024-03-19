using Microsoft.EntityFrameworkCore;
using ProyectoPAU;
using System.Configuration;
using ProyectoPAU.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProyectoPAU.Models;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


      

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IRegistroUsuario, RegistroService>();
  
        builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();

        builder.Services.AddDbContext<TiendauContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

        var key = builder.Configuration.GetValue<string>("JwtSettings:key");
        var keyBytes = Encoding.ASCII.GetBytes(key);

      
        builder.Services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });


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


        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}