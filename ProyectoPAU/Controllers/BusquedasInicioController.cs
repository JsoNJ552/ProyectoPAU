using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Services.ProductosInicioService;
using ProyectoPAU.Models;
using ProyectoPAU.Services.CategoriasService;

namespace ProyectoPAU.Controllers
{
    public class BusquedasInicioController : Controller
    {
        private readonly IProductosInicioService _productosInicioService;
        private readonly ICategoriasService _categoriasService;


        public BusquedasInicioController(IProductosInicioService productosInicioService, ICategoriasService categoriasService)
        {
            _productosInicioService = productosInicioService;
            _categoriasService = categoriasService;
           
                
        }
    
        public async Task<IActionResult>VerProductosTipo(string categoria)
        {
            try
            {

                Console.WriteLine("NOMBRE CATEGORIA = "+categoria);
                List<CategoriaProducto> categorias = await _categoriasService.obtenerProductosAsync();
                ViewData["CategoriasBusqueda"] = categorias;

                List<Producto> productoSeleccionado = await _productosInicioService.getProductosCategoria(categoria);
                return View(productoSeleccionado);

            }
            catch (Exception ex)
            {

                return View();

            }

        }





    }
}
