using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPAU.Models;
using ProyectoPAU.Services.CategoriasService;
using ProyectoPAU.Services.ProductoService.ProductoService;

namespace ProyectoPAU.Controllers
{
	[Authorize(Roles = "Administrador,Empleado")]
    public class AdminInventarioController : Controller
    {

		private readonly TiendauContext _context;
		private readonly IProductService _productoService;
		private readonly ICategoriasService _categoriasService;
		private readonly Producto _producto;

		public AdminInventarioController(TiendauContext context, IProductService productService, ICategoriasService categoriaService)
		{
			_context = context;
			_productoService = productService;
			_categoriasService = categoriaService;



		}
		public async Task  <IActionResult> Index()
        {

            try
            {	int indice = 0;

				List<int> numeros = new List<int>();
				var categorias = await _categoriasService.obtenerProductosAsync();
				foreach(var nameCategoria in categorias)
				{
					
					int cantidad = await _productoService.obtenerCantidadProductosPorNombre(nameCategoria.Nombre);
					nameCategoria.IdCategoria = cantidad;
					numeros.Add(cantidad);

				}

				ViewData["cantidad"] = numeros[0];

				return View(categorias);



            }catch (Exception ex)
            {


            }
            return View();
        }
    }
}
