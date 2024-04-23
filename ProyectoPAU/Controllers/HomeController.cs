using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPAU.Models;
using ProyectoPAU.Models.Custom;
using ProyectoPAU.Services.CarService;
using ProyectoPAU.Services.CategoriasService;
using ProyectoPAU.Services.ProductoService.ProductoService;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace ProyectoPAU.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productoService;
        private readonly ICategoriasService _categoriasService;
        private readonly TiendauContext _tiendauContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICarService _carService;
        private readonly Carrito carrito;

        public HomeController(ILogger<HomeController> logger,
            ICategoriasService categoriasService, IProductService productoService, 
            IHttpContextAccessor httpContextAccessor, TiendauContext tiendauContext,
            ICarService carService, Carrito carrito)
        {
            _logger = logger;
            _productoService = productoService;
            _httpContextAccessor = httpContextAccessor;
            _categoriasService = categoriasService;
            _tiendauContext = tiendauContext;
            _carService = carService;
            this.carrito = carrito;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                //SECTION CARRITO CREA EL CARRITO
                var carritoDetalle = HttpContext.Items["Carrito"] as List<CarritoDetalle>;
                //Crea el carrito automaticamente a un usuario recien registrado
                

                if (User.Identity.IsAuthenticated)
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    int? usuarioIDNullable = httpContext.Session.GetInt32("idUsuario");
                    var carritoNullable = await _carService.BuscarAsyncCarrito((int)usuarioIDNullable);

                    if (usuarioIDNullable != null && carritoNullable == null)
                    {
                        var nuevoCarrito = new Carrito
                        {
                            UsuarioId = usuarioIDNullable.Value,

                        };
                        await _carService.CrearCarrito(nuevoCarrito);
                    }
                    int cantidadProductosCarro = 0;
                    foreach (var cantidadProductosCarrito in carritoDetalle as List<CarritoDetalle>)
                    {
                        cantidadProductosCarro = cantidadProductosCarro + (int)cantidadProductosCarrito.Cantidad;

                    }

                    ViewData["CantidadProductos"] = cantidadProductosCarro;

                }



               
               /* ;

               SECCION Categorias

               */

                
                ViewData["Carrito"] = carritoDetalle;

                List<CategoriaProducto> listaCategorias = await _categoriasService.obtenerProductosAsync();



                // Pasar la lista de categorías a la vista
                ViewData["Categorias"] = listaCategorias;
                //ViewData["Carrito"] = CarritoDetalles;


                // Obtener todos los productos de la base de datos
                var listaProductos = await _productoService.obtenerProductosAsync();

                // Iterar sobre cada producto para guardar su imagen
                foreach (var producto in listaProductos)
                {
                    if (!string.IsNullOrEmpty(producto.Foto))
                    {
                        // Convertir la imagen de Base64 a array de bytes
                        byte[] bytes = Convert.FromBase64String(producto.Foto);

                        // Guardar la imagen en el sistema de archivos
                        string imagePath = $"wwwroot/images/Productos/{producto.IdProducto}.png";
                        System.IO.File.WriteAllBytes(imagePath, bytes);
                    }
                }

                // Redirigir a la vista con la lista de productos
                return View(listaProductos);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                // Puedes agregar lógica adicional aquí según sea necesario
                return Content("Error: " + ex.Message);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult AboutUs()
        {
            return View();
        }



        public IActionResult Search(string searchText, int categoryId)
        {
            // Lógica para realizar la búsqueda de productos con el texto y la categoría especificados
            IQueryable<Producto> query = _tiendauContext.Productos;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(p => p.Nombre.Contains(searchText));
            }

            if (categoryId != 0)
            {
                query = query.Where(p => p.IdCategoria == categoryId);
            }

            // Ejecutar la consulta y retornar los resultados como lista
            List<Producto> productos = query.ToList();
            return PartialView("_ProductListPartial", productos);
        }



        [HttpPost]
        public async Task<IActionResult> CerrarSesion()
        {
            // Lógica para cerrar sesión
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
