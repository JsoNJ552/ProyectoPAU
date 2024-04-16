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

                return View(ex);

            }

        }



        public async Task <IActionResult> Computadoras()
        {
            try
            {

                List<Producto> computadoras = await _productosInicioService.getProductosCategoriaComputadora();
                return View(computadoras);

            }catch (Exception ex) {

                return View(ex);

            }

        }
     
        public async Task<IActionResult> TV()
        {
            try
            {

                List<Producto> tv = await _productosInicioService.getProductosCategoriaTelevision();
                return View(tv);

            }
            catch (Exception ex)
            {

                return View(ex);

            }

        }
    
        public async Task<IActionResult> Relojes()
        {
            try
            {

                List<Producto> relojes = await _productosInicioService.getProductosCategoriaRelojes();
                return View(relojes);

            }
            catch (Exception ex)
            {

                return View(ex);

            }

        }

     
        public async Task<IActionResult> oCelulares()
        {
            try
            {

                List<Producto> celulares = await _productosInicioService.getProductosCategoriaCelular();
                return View(celulares);

            }
            catch (Exception ex)
            {

                return View(ex);

            }

        }


    }
}
