using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using ProyectoPAU.Services.VentasService;
using Microsoft.AspNetCore.Http;
using ProyectoPAU.Services.CarService;
using ProyectoPAU.Services.ProductoService.ProductoService;

namespace ProyectoPAU.Controllers
{
    public class CheckOutController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVentasServicecs _ventaServices;
        private readonly ICarService _carService;
        private readonly CarritoDetalle _carritoDetalle;
        private readonly IProductService _productService;

        public CheckOutController(IHttpContextAccessor HttpContextAccessor, IProductService productService,ICarService carService , IVentasServicecs ventaServices,CarritoDetalle carritoDetalle)
        {
            
            _httpContextAccessor =  HttpContextAccessor;
            _ventaServices = ventaServices;
            _carService = carService;
            _carritoDetalle = carritoDetalle;
            _productService = productService;
        }
        public IActionResult Index()
        {
            var carritoDetalle = HttpContext.Items["Carrito"] as List<CarritoDetalle>;
            ViewData["Carrito"] = carritoDetalle;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult>ProcesarVenta([FromBody] DetalleVenta formData)
        {
            try
            {
                // Obtener los detalles del carrito desde HttpContext.Items
                var carritoDetalle = HttpContext.Items["Carrito"] as List<CarritoDetalle>;



                int? usuarioIDNullable = HttpContext.Session.GetInt32("idUsuario");

                // Crear una nueva instancia de Venta y asignar los valores necesarios
                var venta = new Venta
                {
                    TiendaId = 1,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    UsuarioId = usuarioIDNullable,
                };

               

                // Crear una lista para almacenar todos los detalles de venta
                var detallesVenta = new List<DetalleVenta>();

                // Iterar sobre cada elemento del carrito y crear un DetalleVenta para cada uno
                foreach (var cd in carritoDetalle)
                {
                    detallesVenta.Add(new DetalleVenta
                    {
                        // Asignar los valores de cada detalle de venta
                        ProductoId = cd.IdProducto,
                        Cantidad = cd.Cantidad,
                        PrecioUnitario = cd.PrecioUnitario,
                        PrecioTotal = cd.PrecioTotal,
                        NombreJuridico = formData.NombreJuridico,
                        ApellidoJuridico = formData.ApellidoJuridico,
                        Direccion = formData.Direccion,
                        ZipCode = formData.ZipCode,
                        TipoPago = formData.TipoPago,
                        // Asignar otros valores del detalle de venta si es necesario
                    });

                    var productoEditar = new Producto
                    {
                        IdProducto = (int)cd.IdProducto,
                        Cantidad = cd.Cantidad
                    };
                    await _productService.EditarCantidadProductos(productoEditar);

                    

                }

                // Llamar al método RegistrarVenta para procesar la venta con todos los detalles
                await _ventaServices.RegistrarVenta(venta, detallesVenta);

                var carritoNullable = await _carService.BuscarAsyncCarrito((int)usuarioIDNullable);





                ;

                if (carritoNullable != null)
                {

                    var nuevoCarritoDetalle = new CarritoDetalle
                    {
                        IdCarrito = carritoNullable.Id

                    };

                    await _carService.EliminarCarritosDetalles((int)nuevoCarritoDetalle.IdCarrito);

                    Console.WriteLine("existe un carrito para el usuario logueado" + " " + "Precio total" + " " + nuevoCarritoDetalle.PrecioTotal);
                }

                // Limpiar el carrito después de procesar la venta

                // Redirigir a una vista de confirmación u otra acción
                return Ok();
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente
                Console.WriteLine($"Error al procesar la venta: {ex.Message}");
                // Redirigir a una vista de error u otra acción
                return BadRequest();
            }
        }




    }
}
