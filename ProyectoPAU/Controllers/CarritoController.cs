using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.CarService;

namespace ProyectoPAU.Controllers
{
    public class CarritoController : Controller
    {

        private readonly ICarService _carService;
        private readonly IHttpContextAccessor _httpContextAccesso;
        private readonly Carrito _carrito;
        private readonly CarritoDetalle _carritoDetalle;

        public CarritoController(ICarService carService, Carrito carrito, IHttpContextAccessor httpContextAccesso, CarritoDetalle carritoDetalle)
        {
            _carService = carService;
            _httpContextAccesso = httpContextAccesso;
            _carritoDetalle = carritoDetalle;
            _carrito = carrito;

        }


         public    IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito([FromBody]CarritoDetalle carritoDe)
        {
            try
            {

                var httpContext = _httpContextAccesso.HttpContext;
                // Aquí puedes acceder a las propiedades de CarritoDetalle, como carrito.IdProducto y carrito.Cantidad
                var cantidadProductos = carritoDe.Cantidad;
                var  productoID = carritoDe.IdProducto;
                var precioUnitario = carritoDe.PrecioUnitario;
                var precioTotal = precioUnitario * cantidadProductos;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");


                Console.Write("Cantidad Productos" + " " + cantidadProductos + "Usuario " + usuarioIDNullable);

                var carritoNullable = await _carService.BuscarAsyncCarrito((int)usuarioIDNullable);

                

               

;

                if(carritoNullable != null)
                {

                    var nuevoCarritoDetalle = new CarritoDetalle
                    {
                        IdCarrito = carritoNullable.Id,
                        IdProducto = productoID,
                        Cantidad = cantidadProductos,
                        PrecioUnitario = precioUnitario,
                        PrecioTotal = precioTotal
                    };



                    await _carService.addCarrito(nuevoCarritoDetalle);

                    Console.WriteLine("existe un carrito para el usuario logueado" + " " + "Precio total"+" "+ nuevoCarritoDetalle.PrecioTotal);
                }
               


                // Luego, puedes llamar al servicio de carrito para agregar el producto al carrito

                // Si la operación fue exitosa, puedes devolver un mensaje de éxito o algún otro resultado
                return Ok("Producto agregado al carrito correctamente");
            }
            catch (Exception ex)
            {
                // En caso de error, puedes devolver un mensaje de error
                return StatusCode(500, $"Error al agregar el producto al carrito: {ex.Message}");
            }



        }




        [HttpPost]
        public async Task<IActionResult>Eliminar(int IdProducto)
        {
            try
            {
                await _carService.EliminarCarritoDetalle(IdProducto);
                return RedirectToAction("Index","home");
            }
            catch (Exception ex)
            {


                Console.WriteLine($"Error eeeeeeerrrrrrr: {ex.Message}"); 
                // Manejar la excepción según sea necesario
                return RedirectToAction("Index","home");
            }
        }





    }
}
