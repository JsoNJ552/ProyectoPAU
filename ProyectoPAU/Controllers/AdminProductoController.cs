using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.CategoriasService;
using ProyectoPAU.Services.ProductoService;
using ProyectoPAU.Services.ProductoService.ProductoService;
using ProyectoPAU.Services.ProductosInicioService;

namespace ProyectoPAU.Controllers
{
    [Authorize(Roles ="Administrador,Empleado")]
    public class AdminProductoController : Controller
    {


        private readonly TiendauContext _context;
        private readonly IProductService _productoService;
        private readonly ICategoriasService _categoriasService;
        private readonly Producto _producto;
        private readonly IProductosInicioService _productosInicioService; 

        public AdminProductoController(TiendauContext context, IProductosInicioService productosInicioService, IProductService productService, ICategoriasService categoriaService)
        {
            _context = context;
            _productoService = productService;
            _categoriasService = categoriaService;
            _productosInicioService = productosInicioService;
            
            

        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
              try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var username = HttpContext.Session.GetString("Nombre");

                // Obtener todos los productos de la base de datos
                var listaProductos = await _productosInicioService.getProductosAcAndInac();

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
        public async Task<IActionResult> ProductoSolo(string name)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var username = HttpContext.Session.GetString("Nombre");

                // Obtener todos los productos de la base de datos
                var listaProductos = await _productosInicioService.obtenerTodosProductosPorNombreAsync(name);

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



        [HttpPost]
        public async Task<IActionResult> GuardarEditarProducto([FromForm] Producto producto, IFormFile photoFile)
        {
            try
            {
                if (producto == null)
                {
                    return BadRequest("No se proporcionó ningún producto para editar.");
                }

              
                    // Manejar el archivo de imagen
                    var productoEditar = await _productoService.obtenerProductosPorId(producto.IdProducto);
                    productoEditar.IdProducto = producto.IdProducto;
                    productoEditar.Nombre = producto.Nombre;
                    productoEditar.Marca = producto.Marca;
                    productoEditar.Cantidad = producto.Cantidad;
                    productoEditar.Tipo = producto.Tipo;
                    productoEditar.DetalleVenta = producto.DetalleVenta;
                    productoEditar.Activo = producto.Activo;
                    productoEditar.Descripcion = producto.Descripcion;
                    productoEditar.Detalles = producto.Detalles;
                    productoEditar.IdCategoria = producto.IdCategoria;
                    productoEditar.Activo  = producto.Activo;
                   
                    Console.WriteLine("Cosas"+productoEditar.Marca);
                     Console.WriteLine(productoEditar.Activo+ "IDCATEGORIA" + " " + productoEditar.IdCategoria);
                Console.WriteLine("Activo"+productoEditar.Activo);
                await _productoService.EditarrProducto(productoEditar, photoFile);
              

                // Actualizar el producto en la base de datos
                

                return Ok("Producto editado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest("Hubo un problema al editar el producto: " + ex.Message);
            }
        }

        
        [HttpGet]
        public async Task <IActionResult> Editar(int IdProducto)
        {
            try
            {

                List<CategoriaProducto> listaCategorias = await _categoriasService.obtenerProductosAsync();

                // Pasar la lista de categorías a la vista
                ViewData["Categorias"] = listaCategorias;
                Console.WriteLine(IdProducto);

                var producto = await _productoService.obtenerProductosPorId(IdProducto);
               
                


               
               
                   
            


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
