using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ProyectoPAU.Models;
using ProyectoPAU.Services.CategoriasService;
using ProyectoPAU.Services.ProductoService.ProductoService;
using Microsoft.AspNetCore.Authorization;
using ProyectoPAU.Services.ProductoService;

namespace ProyectoPAU.Controllers
{
   
    public class ProductoController : Controller
    {

        private readonly TiendauContext _context;
        private readonly IProductService _productService;
        private readonly ICategoriasService _categoriasService;


        public ProductoController(TiendauContext context, IProductService productService, ICategoriasService categoriasService)
        {
            _context = context;
            _productService = productService;
            _categoriasService = categoriasService;
            

        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            // Obtener la lista de categorías desde la base de datos
            List<CategoriaProducto> listaCategorias = await _categoriasService.obtenerProductosAsync();

            // Pasar la lista de categorías a la vista
            ViewData["Categorias"] = listaCategorias;

            return View();
        }




        
        public async Task<IActionResult> BuscarProductos(string searchInput)
        {
            // Aquí defines el filtro para buscar productos por palabras clave
            Func<Producto, bool> filtro = producto => producto.Nombre.Contains(searchInput);

            // Llama al método obtenerProductosFiltro con el filtro definido
            var productosEncontrados = await _productService.obtenerProductosFiltro(filtro);

            // Devuelve los productos encontrados a la vista
        
            return PartialView("_ProductListPartial", productosEncontrados);
        }





        public IActionResult ProductoDetalle(int idProducto)
        {
            try
            {

                var producto = _productService.obtenerProductosPorId(idProducto);

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

            }
            return View();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> RegistrarProducto([FromForm] Producto model, IFormFile photoFile)
        {
            try
            {
                if (photoFile == null || photoFile.Length == 0)
                {
                    return BadRequest("No se proporcionó ningún archivo.");
                }

                // Procesar el archivo de imagen si es necesario
                await _productService.RegistrarProducto(model, photoFile);

                return Ok("Producto registrado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al registrar el producto: " + ex.Message);
            }
        }










    }
    
}
