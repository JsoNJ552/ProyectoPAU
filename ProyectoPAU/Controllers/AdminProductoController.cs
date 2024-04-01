using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.CategoriasService;
using ProyectoPAU.Services.ProductoService;
using ProyectoPAU.Services.ProductoService.ProductoService;

namespace ProyectoPAU.Controllers
{
    [Authorize(Roles ="Administrador")]
    public class AdminProductoController : Controller
    {


        private readonly TiendauContext _context;
        private readonly IProductService _productoService;
        private readonly ICategoriasService _categoriasService;
        private readonly Producto _producto;

        public AdminProductoController(TiendauContext context, IProductService productService, ICategoriasService categoriaService)
        {
            _context = context;
            _productoService = productService;
            _categoriasService = categoriaService;
            
            

        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
              try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var username = HttpContext.Session.GetString("Nombre");

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


        [HttpGet]
        public async Task<IActionResult> Agregar()
        {

            List<CategoriaProducto> listaCategorias = await _categoriasService.obtenerProductosAsync();

            // Pasar la lista de categorías a la vista
            ViewData["Categorias"] = listaCategorias;

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> RegistrarProducto([FromForm] Producto producto, IFormFile photoFile)
        {
            try
            {
                if (photoFile == null || photoFile.Length == 0)
                {
                    return BadRequest("No se proporcionó ningún archivo.");
                }

                producto.Activo = true;
               
                await _productoService.RegistrarProducto(producto, photoFile);
                return Ok("Producto registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al registrar el producto: " + ex.Message);
            }
        }


        public IActionResult Editar(int IdProducto)
        {
            try
            {
                Console.WriteLine(IdProducto);

                var producto = _productoService.obtenerProductosPorId(IdProducto);
                var categoria = _context.CategoriaProductos.Find(producto.IdCategoria);
                ViewData["CategoriaProducto"] = categoria;


               
               
                    if (!string.IsNullOrEmpty(producto.Foto))
                    {
                        // Convertir la imagen de Base64 a array de bytes
                        byte[] bytes = Convert.FromBase64String(producto.Foto);

                        // Guardar la imagen en el sistema de archivos
                        string imagePath = $"wwwroot/images/Productos/{producto.IdProducto}.png";
                        System.IO.File.WriteAllBytes(imagePath, bytes);
                    }
            


                return View(producto);
            }
            catch (Exception ex)
            {
                // Aquí deberías manejar el error apropiadamente en lugar de simplemente ignorarlo.
                Console.WriteLine("Error al editar el producto: " + ex.Message);
                return RedirectToAction("Index", "Error"); // Redirige a una página de error.
            }
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int IdProducto)
        {
            try
            {
                await _productoService.EliminarrProducto(IdProducto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return Content("Error: " + ex.Message);
            }
        }




    }
}
