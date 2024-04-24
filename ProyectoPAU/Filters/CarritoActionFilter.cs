using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using ProyectoPAU.Services.CarService;
using ProyectoPAU.Models;

public class CarritoActionFilter : IAsyncActionFilter
{
    private readonly ICarService _carService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CarritoActionFilter(ICarService carService, IHttpContextAccessor httpContextAccessor)
    {
        _carService = carService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.User.Identity.IsAuthenticated && httpContext.User.Identities != null)
            {
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");
                int usuarioID = usuarioIDNullable ?? 3; // DefaultValue es el valor predeterminado que desees usar en caso de que no haya ningún valor en la sesión

                var Carrito = await _carService.BuscarAsyncCarrito(usuarioID);

                // Verificar si Carrito es null antes de intentar acceder a sus propiedades
                if (Carrito != null)
                {

                

                    Console.WriteLine("El valor es: " + Carrito.UsuarioId);
                //trae todos los productos del carrito
                    var CarritoDetalle = await _carService.detallesCarrito(Carrito);
                    Console.WriteLine("El valor es: SOY DESDE EL FILTROO " + usuarioID);

                    foreach (var carroFoto in CarritoDetalle)
                    {
                        byte[] bytes = Convert.FromBase64String(carroFoto.IdProductoNavigation.Foto);

                        // Generar un nombre de archivo único usando el ID del producto
                        string imageName = $"{carroFoto.IdProductoNavigation.IdProducto}.png";

                        // Guardar la imagen en el sistema de archivos
                        string imagePath = Path.Combine("wwwroot", "images", "Productos", imageName);
                        System.IO.File.WriteAllBytes(imagePath, bytes);
                    }









                    context.HttpContext.Items["Carrito"] = CarritoDetalle;

                 
                }
                else
                {
                    // Manejar el caso en el que no hay ningún carrito para el usuario

                    Console.WriteLine("No se encontró ningún carrito para el usuario.");
                  






            }
        }

        await next(); // Continuar con la ejecución del siguiente filtro o acción
    }
}
