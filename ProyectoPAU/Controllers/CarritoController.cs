using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.CarService;
using ProyectoPAU.Services.ProductoService.ProductoService;

namespace ProyectoPAU.Controllers
{
    public class CarritoController : Controller
    {

        private readonly ICarService _carService;
        private readonly IHttpContextAccessor _httpContextAccesso;
        private readonly IProductService _productService;
        private readonly Carrito _carrito;
        private readonly CarritoDetalle _carritoDetalle;
        

        public CarritoController(ICarService carService, Carrito carrito,IProductService productService, IHttpContextAccessor httpContextAccesso, CarritoDetalle carritoDetalle)
        {
            _carService = carService;
            _httpContextAccesso = httpContextAccesso;
            _carritoDetalle = carritoDetalle;
            _carrito = carrito;
            _productService = productService;

        }


         public    IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito([FromBody] CarritoDetalle carritoDe)
        {
            try
            {
                if (carritoDe.Cantidad > 5)
                {
                    return Ok("CantidadMayor");
                }

                var httpContext = _httpContextAccesso.HttpContext;

                var precioTotal = carritoDe.PrecioUnitario * carritoDe.Cantidad;
                int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");

                var carritoNullable = await _carService.BuscarAsyncCarrito((int)usuarioIDNullable);
                bool cantidadProductosDisponible = await _productService.VerificarCantidadProducots((int)carritoDe.IdProducto, (int)carritoDe.Cantidad);
                if (!cantidadProductosDisponible)
                {
                    return Ok("Nohaysuficiente");
                }
                if (carritoNullable != null)
                {
                    var nuevoCarritoDetalle = new CarritoDetalle
                    {
                        IdCarrito = carritoNullable.Id,
                        IdProducto = carritoDe.IdProducto,
                        Cantidad = carritoDe.Cantidad,
                        PrecioUnitario = carritoDe.PrecioUnitario,
                        PrecioTotal = precioTotal
                    };



                

                    await _carService.addCarrito(nuevoCarritoDetalle);
                    Console.WriteLine("existe un carrito para el usuario logueado" + " " + "Precio total" + " " + nuevoCarritoDetalle.PrecioTotal);
                }

                return Ok("Producto agregado al carrito correctamente");
            }
            catch (Exception ex)
            {
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
